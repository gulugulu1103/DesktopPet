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
    public class JsonDataService : IPetDataServices
    {
        public Pet GetPet(string Name)
        {
            throw new NotImplementedException();
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
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            string Path = System.Environment.CurrentDirectory + @"\Data\pets";
            string JsonFile = Path + $"\\{pet.Name}.json";
            string TempFile = Path + $"\\{pet.Name}_temp.json";
            string BackupPath = Path + "\\backups";
            string BackupFile = BackupPath + $"\\{pet.Name}_backup.json";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            if (!Directory.Exists(BackupPath))
            {
                Directory.CreateDirectory(BackupPath);
            }
            if (File.Exists(JsonFile))
            {
                File.WriteAllText(TempFile, JsonSerializer.Serialize(pet, jsonSerializerOptions));
                File.Replace(TempFile, JsonFile, BackupFile);
            }
            else
            {
                File.WriteAllText(JsonFile, JsonSerializer.Serialize(pet, jsonSerializerOptions));
            }
        }
    }
}
