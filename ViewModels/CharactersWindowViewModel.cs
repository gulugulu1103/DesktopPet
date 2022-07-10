using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;

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

        public CharactersWindowViewModel(IEventAggregator _eventAggregator)
        {
            IPetJsonService jsonService = new JsonService();
            this.pets = jsonService.GetAllPets();
            this.eventAggregator = _eventAggregator;
            this.SelectedIndex = Properties.Settings.Default.SelectedPetIndex;

            this.ApplyPetChangeCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<MainWindowPetChangedEvent>().Publish(pets[SelectedIndex]);
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("CharactersWindow");
            });

            this.CloseCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("CharactersWindow");
            });

        }
    }
}
