using DesktopPet.Models;
using DesktopPet.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopPet.ViewModels
{
    public class CharactersWindowViewModel : BindableBase
    {
        public List<Pet> pets = new List<Pet>();

        private int _selected;
        public int Selected
        {
            get { return _selected; }
            set { _selected = value; this.RaisePropertyChanged(nameof(Selected)); }
        }

        public CharactersWindowViewModel()
        {
            IJsonService<Pet> jsonService = new JsonService<Pet>();
            this.pets = jsonService.GetAll(Environment.CurrentDirectory);
        }
    }
}
