using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    public  class PetDataServices : JsonDataService, IPetDataServices
    {
        public static readonly string PetPath = DataPath + "\\Pets";
        
        public List<Pet> GetAll()
        {
            List<Pet> pets = new List<Pet>();
            DirectoryInfo petFolder = new DirectoryInfo(PetPath);
            foreach (FileInfo file in petFolder.GetFiles())
            {
                if(file.Extension == ".json")
                {
                    Pet pet = null;
                    try
                    {
                        string jsonString = File.ReadAllText(file.FullName);
                        pet = JsonSerializer.Deserialize<Pet>(jsonString)!;
                    }
                    catch(Exception e)
                    {
                        System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    }
                    if(pet != null)
                        pets.Add(pet);
                }
            }
            return pets;
        }

        public void SaveAll(List<Pet> pets)
        {
            foreach (Pet pet in pets)
            {
                SavePet(pet);
            }
        }

        public void SavePet(Pet pet)
        {
            string jsonFile = PetPath + $"\\{pet.Name}.json";
            string tempFile = PetPath + $"\\{pet.Name}_temp.json";
            string backupPath = PetPath + "\\Backups";
            string backupFile = backupPath + $"\\{pet.Name}_backup.json";

            if (!Directory.Exists(PetPath))
            {
                Directory.CreateDirectory(PetPath);
            }
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            if (File.Exists(jsonFile))
            {
                File.WriteAllText(tempFile, JsonSerializer.Serialize(pet, jsonSerializerOptions));
                File.Replace(tempFile, jsonFile, backupFile);
            }
            else
            {
                File.WriteAllText(jsonFile, JsonSerializer.Serialize(pet, jsonSerializerOptions));
            }
        }


    }
}
