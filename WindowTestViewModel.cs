using DesktopPet.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace DesktopPet
{
    public class WindowTestViewModel : BindableBase
    {
        BinaryService binaryServices = new BinaryService();
        public DelegateCommand ButtonCommand { get; private set; }

        public WindowTestViewModel()
        {
            this.ButtonCommand = new DelegateCommand(() =>
            {
                binaryServices.CreateDefaultPetData();
            });
        }
    }
}
