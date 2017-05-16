using BingMapsSDSToolkit;
using BingMapsSDSToolkit.DataSourceAPI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BingSDSTestApp.Views
{
    public partial class DataSourceConverterModal : Window
    {
        public DataSourceConverterModal()
        {
            InitializeComponent();
        }

        private async void Convert_Clicked(object sender, RoutedEventArgs e)
        {
            var inputType = (DataSourceFormat)Enum.Parse(typeof(DataSourceFormat), ((string)(FromFormatCbx.SelectedItem as ComboBoxItem).Content));

            var outputType = (DataSourceFormat)Enum.Parse(typeof(DataSourceFormat), ((string)(ToFormatCbx.SelectedItem as ComboBoxItem).Content));

            string filters, defaultExt;
            Utilities.GetFileExtensions(inputType, out defaultExt, out filters);

            var ofd = new OpenFileDialog()
            {
                DefaultExt = defaultExt,
                Filter = filters
            };

            var b = ofd.ShowDialog();

            if (b.HasValue && b.Value)
            {
                var dataSource = new DataSource();
                using (var s = ofd.OpenFile())
                {
                    if (await dataSource.ReadAsync(s, inputType))
                    {
                        Utilities.GetFileExtensions(outputType, out defaultExt, out filters);

                        var sfd = new SaveFileDialog()
                        {
                            DefaultExt = defaultExt,
                            Filter = filters
                        };

                        b = sfd.ShowDialog();
                        if (b.HasValue && b.Value)
                        {
                            using (var outputStream = sfd.OpenFile())
                            {
                                await dataSource.WriteAsync(outputStream, outputType);
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Data source conversion complete.");
            this.Close();
        }
    }
}
