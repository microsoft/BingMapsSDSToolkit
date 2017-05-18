using BingMapsSDSToolkit;
using BingMapsSDSToolkit.DataSourceAPI;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BingSDSTestApp.Views
{
    public partial class DownloadModal : Window
    {
        private BasicDataSourceInfo info;

        public DownloadModal(DataSourceDetails details)
        {
            InitializeComponent();

            info = new BasicDataSourceInfo()
            {
                AccessId = details.AccessId,
                QueryUrl = details.QueryUrl,
                MasterKey = details.MasterKey
            };

            this.DataContext = info;
        }

        private async void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingPanel.Visibility = System.Windows.Visibility.Visible;
                var dataSource = await new DataSourceManager().Download(info);
                var type = (DataSourceFormat)Enum.Parse(typeof(DataSourceFormat), ((string)(DataSourceFormatCbx.SelectedItem as ComboBoxItem).Content));

                string filters, defaultExt;
                GetFileExtensions(type, out defaultExt, out filters);

                var sfd = new SaveFileDialog()
                {
                    DefaultExt = defaultExt,
                    Filter = filters
                };

                var b = sfd.ShowDialog();
                if (b.HasValue && b.Value)
                {
                    using (var outputStream = sfd.OpenFile())
                    {
                        await dataSource.WriteAsync(outputStream, type);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetFileExtensions(DataSourceFormat format, out string defaultExt, out string filters)
        {
            switch (format)
            {
                case DataSourceFormat.CSV:
                    defaultExt = ".csv";
                    filters = "CSV (*.csv)|*.csv|Text (*.txt)|*.txt|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.TAB:
                case DataSourceFormat.PIPE:
                    defaultExt = ".txt";
                    filters = "Text (*.txt)|*.txt|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.XML:
                default:
                    defaultExt = ".xml";
                    filters = "Xml (.xml)|*.xml";
                    break;
            }
        }
    }
}
