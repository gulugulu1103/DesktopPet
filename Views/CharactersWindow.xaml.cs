using DesktopPet.ViewModels;
using System.Windows;

namespace DesktopPet.Views
{
    /// <summary>
    /// CharactersWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CharactersWindow : Window
    {
        public CharactersWindow()
        {
            InitializeComponent();
            this.petListBox.ItemsSource = (DataContext as CharactersWindowViewModel).pets;
        }
    }
}
