using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using DesktopPet.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DesktopPet.ViewModels
{
    public class CharactersWindowViewModel : BindableBase
    {
        private List<Pet> pets = new List<Pet>();
        public List<Pet> Pets
        {
            get { return pets; }
            set { SetProperty(ref pets, value); }
        }

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

        private void OnCreateWindowClose(string obj)
        {
            petCreateWindow?.Close();
            petCreateWindow = null;
            IPetService petService = new JsonService();
            this.Pets = petService.GetAllPets();
        }


        public CharactersWindowViewModel(IEventAggregator _eventAggregator)
        {
            IPetService petService = new JsonService();
            this.Pets = petService.GetAllPets();
            // 事件订阅
            this.eventAggregator = _eventAggregator;
            eventAggregator.GetEvent<WindowCloseEvent>().Subscribe(OnCreateWindowClose, filter: arg =>
            {
                if (arg == "PetCreateWindow") return true;
                else return false;
            });

            this.SelectedIndex = Properties.Settings.Default.SelectedPetIndex;

            // 应用更改按钮
            this.ApplyPetChangeCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<MainWindowPetChangedEvent>().Publish(Pets[SelectedIndex]);
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
