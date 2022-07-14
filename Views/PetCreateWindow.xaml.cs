using System.Windows;

namespace DesktopPet.Views
{
    /// <summary>
    /// PetCreateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PetCreateWindow : Window
    {
        public PetCreateWindow()
        {
            InitializeComponent();
        }

        private void IconSelector_ImageSelected(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ViewModels.PetCreateWindowViewModel).FileDic[Models.Moves.Icon] = this.IconSelector.Uri.LocalPath;
        }
    }
}
