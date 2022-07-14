using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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

        private string gifFilter = @"动态图像(.gif)|*.gif";
        public string GifFilter
        {
            get { return gifFilter; }
            set { SetProperty(ref gifFilter, value); }
        }

        private string testString = "TestMyTest";
        public string TestString
        {
            get { return testString; }
            set { SetProperty(ref testString, value); }
        }

        private string petName;
        public string PetName
        {
            get { return petName; }
            set { SetProperty(ref petName, value); }
        }

        private string petDescription;
        public string PetDescription
        {
            get { return petDescription; }
            set { SetProperty(ref petDescription, value); }
        }

        private Dictionary<Moves, string?> fileDic = new();
        public Dictionary<Moves, string?> FileDic
        {
            get { return fileDic; }
            set { SetProperty(ref fileDic, value); }
        }

        private readonly IEventAggregator eventAggregator;

        public DelegateCommand OkCommand { get; private set; } 
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand TestCommand { get; private set; }

        public PetCreateWindowViewModel(IEventAggregator _eventAggregator)
        {
            foreach (Moves move in Enum.GetValues(typeof(Moves)))
            {
                FileDic.Add(move, null);
            }
            this.eventAggregator = _eventAggregator;
   

            this.OkCommand = new DelegateCommand(() =>
            {
                //exam
                bool haveBlank = false;
                foreach (Moves move in Enum.GetValues(typeof(Moves)))
                {
                    if (FileDic[move] == null || !File.Exists(FileDic[move])) haveBlank = true;
                }
                if(haveBlank || PetName == null || PetDescription == null)
                {
                    MessageBox.Show($"输入信息有误，请重试");
                    return;
                }


                if (isSealedPet)
                {
                    var pet = new SealedPet();
                    pet.Name = this.PetName;
                    pet.Description = this.PetDescription;
                    foreach (Moves move in Enum.GetValues(typeof(Moves)))
                    {
                        pet.ImageBytes[move] = File.ReadAllBytes(FileDic[move]);
                    }
                    IPetService service = new BinaryService();
                    service.SavePetData(pet);
                    MessageBox.Show($"已生成封装宠物{pet.Name}");
                }
                else
                {
                    var pet = new Pet();
                    pet.Name = this.PetName;
                    pet.Description = this.PetDescription;
                    pet.ImageSource = new Dictionary<Moves, string?>(FileDic);
                    string CopyFile = null;
                    IPetService service = new JsonService();
                    service.SavePetData(pet);
                    foreach(Moves move in Enum.GetValues(typeof(Moves)))
                    {
                        CopyFile = Path.Combine(Environment.CurrentDirectory, $"Data\\Pets\\{pet.Name}\\{FileDic[move].Split('\\').Last()}");
                        File.Copy(FileDic[move], CopyFile);
                    }
                    MessageBox.Show($"已生成宠物{pet.Name}");
                }
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("PetCreateWindow");
            });
            this.CancelCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<WindowCloseEvent>().Publish("PetCreateWindow");
            });
            this.TestCommand = new DelegateCommand(() =>
            {
                System.Text.StringBuilder sb = new();
                foreach (Moves move in Enum.GetValues(typeof(Moves)))
                {
                    if(move == Moves.Icon)
                        FileDic[move] = $@"C:\Users\gulugulu1103\OneDrive\DesktopPet\bin\Debug\net6.0-windows\Data\Pets\样例\Icon.jpg";
                    else
                        FileDic[move] = $@"C:\Users\gulugulu1103\OneDrive\DesktopPet\bin\Debug\net6.0-windows\Data\Pets\样例\{move}.gif";
                    sb.AppendLine(FileDic[move]);
                }
                MessageBox.Show(sb.ToString());
            });
        }
    }
}
