using BingMapsSDSToolkit;
using BingMapsSDSToolkit.DataSourceAPI;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BingSDSTestApp.Views
{
    public partial class UploadModal : Window
    {
        private DataSource _dataSource;
        private BasicDataSourceInfo _info;
        private bool canClose = true;
        private Stream _dataSourceStream;

        public UploadModal(BasicDataSourceInfo info)
        {
            InitializeComponent();

            _info = new BasicDataSourceInfo()
            {
                AccessId = info.AccessId,
                DataSourceName = info.DataSourceName,
                EntityTypeName = info.EntityTypeName,
                MasterKey = info.MasterKey,
                QueryKey = info.QueryKey
            };

            this.DataContext = _info;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!canClose)
            {
                base.OnClosing(e);
                e.Cancel = true;
            }
        }

        private async void SelectFile_Clicked(object sender, RoutedEventArgs e)
        {
            var type = (DataSourceFormat)Enum.Parse(typeof(DataSourceFormat), ((string)(DataSourceFormatCbx.SelectedItem as ComboBoxItem).Content));

            string filters, defaultExt;
            FileExtensionUtilities.GetFileExtensions(type, out defaultExt, out filters);

            var ofd = new OpenFileDialog()
            {
                DefaultExt = defaultExt,
                Filter = filters
            };

            var b = ofd.ShowDialog();

            if (b.HasValue && b.Value)
            {
                if (type == DataSourceFormat.SHP || type == DataSourceFormat.KML)
                {
                    _dataSource = null;
                    _dataSourceStream = ofd.OpenFile();
                    UploadBtn.IsEnabled = true;
                    EntityTypeNameTbx.IsEnabled = false;
                }
                else
                {
                    _dataSourceStream = null;
                    EntityTypeNameTbx.IsEnabled = true;

                    var dataSource = new DataSource();
                    using (var s = ofd.OpenFile())
                    {
                        if (await dataSource.ReadAsync(s, type))
                        {
                            _dataSource = dataSource;
                            _dataSource.GeocodeStatusChanged += (msg) =>
                            {
                                OutputTbx.Text += msg + "\r\n";
                            };

                            if (type == DataSourceFormat.XML)
                            {
                                _info.DataSourceName = _dataSource.Info.DataSourceName;
                            }

                            _info.EntityTypeName = _dataSource.Info.EntityTypeName;

                            this.DataContext = null;
                            this.DataContext = _info;

                            UploadBtn.IsEnabled = true;
                            ValidateBtn.IsEnabled = true;
                        }
                    }
                }
            }
        }
        
        private async void Validate_Clicked(object sender, RoutedEventArgs e)
        {
            if (_dataSource != null)
            {
                var validation = await _dataSource.Validate();

                var sb = new StringBuilder();

                if (validation.AllRowsHaveLocationInfo &&
                    SkipGeocoding.IsChecked.Value &&
                    !SkipEmptyLocations.IsChecked.Value)
                {
                    sb.AppendLine("Not all rows have locations. Consider geocoding, or selecting the option to skip empty locations.");
                }

                if (validation.Errors != null && validation.Errors.Count > 0)
                {
                    sb.AppendLine("\r\nErrors:");
                    foreach (var er in validation.Errors)
                    {
                        sb.AppendLine(er);
                    }
                }

                if (sb.Length == 0)
                {
                    sb.AppendLine("Validation successful.");
                }

                MessageBox.Show(sb.ToString());
            }
        }

        private async void Upload_Clicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_info.DataSourceName))
            {
                MessageBox.Show("Invalid Data Source Type Name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(MasterKeyTbx.Text))
            {
                MessageBox.Show("Invalid Master Key specified.");
                return;
            }

            var type = (DataSourceFormat)Enum.Parse(typeof(DataSourceFormat), ((string)(DataSourceFormatCbx.SelectedItem as ComboBoxItem).Content));

            if (type != DataSourceFormat.SHP && type != DataSourceFormat.KML)
            {
                if (string.IsNullOrEmpty(_info.EntityTypeName))
                {
                    MessageBox.Show("Invalid Entity Type Name.");
                    return;
                }

                var validation = await _dataSource.Validate();

                if (!validation.AllRowsHaveLocationInfo && !SkipEmptyLocations.IsChecked.Value && !SkipGeocoding.IsChecked.Value)
                {
                    MessageBox.Show("Not all rows have location data.");
                    return;
                }

                try
                {
                    _dataSource.Info.AccessId = _info.AccessId;
                    _dataSource.Info.DataSourceName = _info.DataSourceName;
                    _dataSource.Info.EntityTypeName = _info.EntityTypeName;
                    _dataSource.Info.Description = _info.Description;
                    _dataSource.Info.MasterKey = _info.MasterKey;
                    _dataSource.Info.QueryKey = _info.QueryKey;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }          

            LoadingPanel.Visibility = System.Windows.Visibility.Visible;
            canClose = false;

            OutputTbx.Text = string.Empty;
            
            var dataSourceManager = new DataSourceManager();
            dataSourceManager.UploadStatusChanged += (msg) =>
            {
                OutputTbx.Text += msg + "\r\n";
            };
            
            if (!SkipGeocoding.IsChecked.Value && type != DataSourceFormat.SHP && type != DataSourceFormat.KML)
            {
                var r = await _dataSource.Geocode(MasterKeyTbx.Text);

                if (!string.IsNullOrWhiteSpace(r.Error))
                {
                    MessageBox.Show("Error during Geocoding: \r\n" + r.Error);
                    return;
                }
                else if (r.Failed == 0 || (r.Failed > 0 && SkipEmptyLocations.IsChecked.Value))
                {
                    var loadOperation = (LoadOperation)Enum.Parse(typeof(LoadOperation), ((string)(LoadOperationCbx.SelectedItem as ComboBoxItem).Content));
                    
                    var dataflow = await dataSourceManager.Upload(_dataSource, loadOperation, MakePublicCbx.IsChecked.Value, SkipEmptyLocations.IsChecked.Value);

                    if (!string.IsNullOrWhiteSpace(dataflow.ErrorMessage))
                    {
                        MessageBox.Show(dataflow.ErrorMessage);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Upload successful.");
                    }
                }
                else
                {
                    MessageBox.Show("Not all rows could be geocoded. Upload aborted. Consider skipping empty locations.");
                    return;
                }
            }
            else
            {
                var loadOperation = (LoadOperation)Enum.Parse(typeof(LoadOperation), ((string)(LoadOperationCbx.SelectedItem as ComboBoxItem).Content));

                DataflowJob dataflow;

                if (type == DataSourceFormat.SHP || type == DataSourceFormat.KML)
                {
                    dataflow = await dataSourceManager.Upload(_dataSourceStream, type, loadOperation, _info, MakePublicCbx.IsChecked.Value);
                    _dataSourceStream.Dispose();
                }
                else
                {
                    dataflow = await dataSourceManager.Upload(_dataSource, loadOperation, MakePublicCbx.IsChecked.Value, SkipEmptyLocations.IsChecked.Value);
                }

                if (!string.IsNullOrWhiteSpace(dataflow.ErrorMessage))
                {
                    MessageBox.Show(dataflow.ErrorMessage);
                    LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
                    canClose = true;
                    return;
                }
                else
                {
                    MessageBox.Show("Upload successful.");
                }
            }

            canClose = true;
            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;

            this.Close();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
