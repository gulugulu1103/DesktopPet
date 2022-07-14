using DesktopPet.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace DesktopPet
{
    public class WindowTestViewModel : BindableBase
    {
        private string myPath;
        public string MyPath
        {
            get { return myPath; }
            set { SetProperty(ref myPath, value); }
        }

        public DelegateCommand ButtonCommand { get; private set; }

        public WindowTestViewModel()
        {
            this.ButtonCommand = new DelegateCommand(() =>
            {
                MessageBox.Show(MyPath);
            });
        }
    }
}
