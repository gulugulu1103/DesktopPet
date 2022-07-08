using System.Windows;

namespace DesktopPet
{
    /// <summary>
    /// WindowTest.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTest : Window
    {
        public WindowTest()
        {
            InitializeComponent();
            this.DataContext = new WindowTestViewModel();
        }


    }
}
