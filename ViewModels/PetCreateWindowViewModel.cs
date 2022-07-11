using Prism.Commands;
using Prism.Mvvm;

namespace DesktopPet.ViewModels
{
    public class PetCreateWindowViewModel : BindableBase
    {
        private bool isSealedPet = false;
        public bool IsSealedPet
        {
            get { return isSealedPet; }
            set { SetProperty(ref isSealedPet, value); }
        }

        public DelegateCommand OkCommand { get; private set; }
        private void OkCommandExecute()
        {

        }

        public PetCreateWindowViewModel()
        {
            this.OkCommand = new DelegateCommand(OkCommandExecute);
        }
    }
}
