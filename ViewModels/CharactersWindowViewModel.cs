using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using DesktopPet.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace DesktopPet.ViewModels
{
    public class CharactersWindowViewModel : BindableBase
    {
        public List<Pet> pets = new List<Pet>();
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        private readonly IEventAggregator eventAggregator;
        public DelegateCommand ApplyPetChangeCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand CreateCommand { get; private set; }
        PetCreateWindow petCreateWindow { get; set; } = null;

        public CharactersWindowViewModel(IEventAggregator _eventAggregator)
        {
            IPetService petService = new JsonService();
            this.pets = petService.GetAllPets();
            this.eventAggregator = _eventAggregator;
            this.SelectedIndex = Properties.Settings.Default.SelectedPetIndex;

            // 应用更改按钮
            this.ApplyPetChangeCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<MainWindowPetChangedEvent>().Publish(pets[SelectedIndex]);
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("CharactersWindow");
            });

            // 关闭/取消按钮
            this.CloseCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("CharactersWindow");
            });

            // 新建宠物按钮，弹出新建宠物窗体
            this.CreateCommand = new DelegateCommand(() =>
            {
                if (petCreateWindow == null)
                {
                    petCreateWindow = new PetCreateWindow();
                    petCreateWindow.ShowDialog();
                    petCreateWindow = null;
                }
                else
                {
                    petCreateWindow.Activate();
                }

            });

        }
    }
}
