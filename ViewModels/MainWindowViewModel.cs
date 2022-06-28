using DesktopPet.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopPet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Desktop Pet";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _closeButtonImg = "/Resources/close_0.png";
        public string CloseButtonImg
        {
            get { return _closeButtonImg; }
            set { _closeButtonImg = value; this.RaisePropertyChanged(nameof(CloseButtonImg)); }
        }

        public DelegateCommand CloseButtonCommand { get; set; }
        private void CloseButtonCommandExecute()
        {
            System.Environment.Exit(0);
        }

        public DelegateCommand MouseEnterCommand { get; set; }
        private void MouseEnterCommandExecute()
        {
            this.CloseButtonImg = "/Resources/close_1.png";
        }
        public DelegateCommand MouseLeaveCommand { get; set; }
        private void MouseLeaveCommandExecute()
        {
            this.CloseButtonImg = "/Resources/close_0.png";
        }

        private Pet _pet;
        public Pet Pet
        {
            get { return _pet; }
            set { _pet = value; this.RaisePropertyChanged(nameof(Pet)); }
        }

        

        public MainWindowViewModel()
        {

            this.CloseButtonCommand = new DelegateCommand(this.CloseButtonCommandExecute);
            this.Pet = new Pet();
        }
    }
}
