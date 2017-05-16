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
                QueryURL = details.QueryURL,
                MasterKey = details.MasterKey
            };

            this.DataContext = info;
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                new DataSourceManager().DeleteDataSource(info);
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
