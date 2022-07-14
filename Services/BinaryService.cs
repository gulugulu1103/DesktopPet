using DesktopPet.Models;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesktopPet.Services
{
    /// <summary>
    /// 该类以二进制形式
    /// </summary>
    class BinaryService : IPetService
    {
        private static readonly string CurrentPath = Environment.CurrentDirectory;
        private static readonly string BackupPath = Path.Combine(CurrentPath, "Backups");
        private static readonly MessagePackSerializerOptions SeializerOptions = MessagePackSerializerOptions.Standard.WithSecurity(MessagePackSecurity.UntrustedData);

        public List<Pet> GetAllPets()
        {
            return GetAllPets(Path.Combine(CurrentPath, "Data"));
        }
        public List<Pet> GetAllPets(string dataPath)
        {
            string petsPath = Path.Combine(dataPath, "Pets");
            List<Pet> pets = new List<Pet>();
            FileInfo[] petFiles = (new DirectoryInfo(petsPath)).GetFiles();
            foreach (var file in petFiles)
            {
                if (file.Extension == ".pet")
                {
                    SealedPet sealedPet = null;
                    try
                    {
                        sealedPet = MessagePackSerializer.Deserialize<SealedPet>(File.ReadAllBytes(file.FullName), SeializerOptions);
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                        Environment.Exit(0);
                    }
                    if (sealedPet != null)
                    {
                        pets.Add(sealedPet);
                    }
                }
            }
            return pets;

        }

        public void SavePetData(Pet pet)
        {
            SavePetData(pet, Path.Combine(CurrentPath, "Data"));
        }
        public void SavePetData(Pet pet, string dataPath)
        {
            SealedPet sealedPet = pet as SealedPet;
            string petsPath = Path.Combine(dataPath, "Pets");
            string petFile = Path.Combine(petsPath, $"{sealedPet.Name}.pet");
            string tempFile = Path.Combine(petsPath, $"{sealedPet.Name}_tempdata.pet");
            string backupFile = Path.Combine(BackupPath, $"{sealedPet.Name}_backupdata.pet");
            var imageBytes = sealedPet.ImageBytes;
            foreach (Moves move in Enum.GetValues(typeof(Moves)))
            {
                if (imageBytes.ContainsKey(move) && imageBytes[move] != null)
                {
                    continue;
                }
                else
                {
                    imageBytes[move] = null;
                }
            }
            if (!Directory.Exists(petsPath))
            {
                Directory.CreateDirectory(petsPath);
            }
            if (File.Exists(petFile))
            {
                File.WriteAllBytes(tempFile, MessagePackSerializer.Serialize(sealedPet));
                File.Replace(tempFile, petFile, backupFile);
            }
            else
            {
                System.Windows.MessageBox.Show(petFile);
                File.WriteAllBytes(petFile, MessagePackSerializer.Serialize(sealedPet));
            }
        }

        public void CreateDefaultPetData()
        {
            SealedPet sealedPet = new InitService().CreateSamplePet<SealedPet>() as SealedPet;
            sealedPet.Name = "Sealed";
            var imgdic = sealedPet.ImageBytes = new Dictionary<Moves, byte[]?>();
            imgdic.Add(Moves.Icon, Properties.Resources.SamplePet_Icon);
            imgdic.Add(Moves.Stand, Properties.Resources.SamplePet_Stand);
            imgdic.Add(Moves.LieDown, Properties.Resources.SamplePet_LieDown);
            imgdic.Add(Moves.MoveLeft, Properties.Resources.SamplePet_MoveLeft);
            imgdic.Add(Moves.MoveRight, Properties.Resources.SamplePet_MoveRight);
            imgdic.Add(Moves.MoveUp, Properties.Resources.SamplePet_MoveUp);
            imgdic.Add(Moves.MoveDown, Properties.Resources.SamplePet_MoveDown);
            SavePetData(sealedPet);
        }
        public BinaryService()
        {
            if (!Directory.Exists(BackupPath))
            {
                Directory.CreateDirectory(BackupPath);
            }
            if (!Directory.Exists(CurrentPath + "\\Data\\Pets"))
            {
                Directory.CreateDirectory(CurrentPath + "\\Data\\Pets");
            }
            if (!Directory.Exists(CurrentPath + "\\Saves"))
            {
                Directory.CreateDirectory(CurrentPath + "\\Saves");
            }
        }
    }
}
