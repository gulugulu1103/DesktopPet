using DesktopPet.Models;
using System;
using System.Collections.Generic;

namespace DesktopPet.Services
{
    public class InitService : IInitService
    {
        private static readonly string CurrentPath = Environment.CurrentDirectory;
        private JsonService JsonService = new JsonService();
        public void FirstInit()
        {
            Properties.Settings.Default.FirstStart = false;
            Properties.Settings.Default.Save();
            JsonService.CreateDefaultSettings();
            JsonService.CreateDefaultPetData();
        }

        public void EnvironmentCheck()
        {
            var petsFolderInfo = new System.IO.DirectoryInfo(CurrentPath + "\\Data\\Pets");
            if (petsFolderInfo.GetDirectories().Length == 0)
            {
                JsonService.CreateDefaultPetData();
            }

            if (!System.IO.File.Exists(CurrentPath + "\\Saves\\Settings.json"))
            {
                JsonService.CreateDefaultSettings();
            }

        }

        public Pet CreateSamplePet<T>()
        {
            // 调用Pet的构造函数
            var samplePet = new Pet();
            samplePet.Name = Properties.Resources.SamplePetName;
            samplePet.Description = Properties.Resources.SamplePetDescription;
            samplePet.ImageSource = new Dictionary<Moves, string?>();
            samplePet.ImageSource.Add(Moves.Icon, "Icon.jpg");
            samplePet.ImageSource.Add(Moves.Stand, "Stand.gif");
            samplePet.ImageSource.Add(Moves.LieDown, "LieDown.gif");
            samplePet.ImageSource.Add(Moves.MoveLeft, "MoveLeft.gif");
            samplePet.ImageSource.Add(Moves.MoveRight, "MoveRight.gif");
            samplePet.ImageSource.Add(Moves.MoveUp, "MoveDown.gif");
            samplePet.ImageSource.Add(Moves.MoveDown, "MoveDown.gif");
            return samplePet;
        }



    }
}
