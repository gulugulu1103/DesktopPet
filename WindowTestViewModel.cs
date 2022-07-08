using DesktopPet.Models;
using DesktopPet.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DesktopPet
{
    public class WindowTestViewModel : BindableBase
    {
        IPetService<Pet> jsonService = new JsonService<Pet>();
        private List<Pet> _pets;
        public List<Pet> Pets
        {
            get { return _pets; }
            set { _pets = value; this.RaisePropertyChanged(nameof(Pets)); }
        }

        private string _info;

        public string Info
        {
            get { return _info; }
            set { _info = value; this.RaisePropertyChanged(nameof(Info)); }
        }


        public WindowTestViewModel()
        {
            Pets = jsonService.GetAll(Environment.CurrentDirectory);
            // MessageBox.Show(Pets[0].ToString());
        }
    }
}
