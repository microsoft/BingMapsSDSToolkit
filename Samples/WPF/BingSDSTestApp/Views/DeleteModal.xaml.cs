using BingMapsSDSToolkit;
using BingMapsSDSToolkit.DataSourceAPI;
using System;
using System.Windows;

namespace BingSDSTestApp.Views
{
    public partial class DeleteModal : Window
    {
        private BasicDataSourceInfo info;

        public DeleteModal(DataSourceDetails details)
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
                await new DataSourceManager().DeleteDataSource(info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
