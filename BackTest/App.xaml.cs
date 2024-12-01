using BackTest.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BackTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainvm = new MainViewModel();
            var mw = new MainWindow()
            {
                DataContext = mainvm,
            };
            mw.Show();
        }
    }

}
