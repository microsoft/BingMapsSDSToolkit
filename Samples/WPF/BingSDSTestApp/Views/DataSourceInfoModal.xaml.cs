using BingMapsSDSToolkit;
using System.Windows;

namespace BingSDSTestApp.Views
{
    public partial class DataSourceInfoModal : Window
    {
        #region Constructor

        public DataSourceInfoModal()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
