using DesktopPet.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace DesktopPet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<CharactersWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ;
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
