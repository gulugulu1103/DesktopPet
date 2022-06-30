using DesktopPet.Models;
using DesktopPet.Views;
using Prism.Commands;
using Prism.Mvvm;

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

        private string _imageNow;

        public string ImageNow
        {
            get { return _imageNow; }
            set { _imageNow = value; this.RaisePropertyChanged(nameof(ImageNow)); }
        }


        public DelegateCommand CloseButtonCommand { get; set; }
        private void CloseButtonCommandExecute()
        {
            System.Environment.Exit(0);
        }

        public DelegateCommand CharactersButtonCommand { get; set; }
        private void CharactersButtonCommandExecute()
        {
            CharactersWindow charactersWindow = new CharactersWindow();
            charactersWindow.Show();
        }

        private Pet _pet;
        public Pet Pet
        {
            get { return _pet; }
            set { _pet = value; this.RaisePropertyChanged(nameof(Pet)); }
        }

        public MainWindowViewModel()
        {
            this.CharactersButtonCommand = new DelegateCommand(this.CharactersButtonCommandExecute);
            this.CloseButtonCommand = new DelegateCommand(this.CloseButtonCommandExecute);
            ImageNow = @"\Views\Resources\Icons\Icon.png";
        }
    }
}
