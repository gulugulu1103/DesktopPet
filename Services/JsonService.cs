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
    public class JsonService<TData> : IJsonService<TData>
    {
        public static readonly string DataPath = Environment.CurrentDirectory + "\\Data";
        public static readonly string SavePath = DataPath + "\\Saves";
        public static readonly string PetPath = DataPath + "\\Pets";
        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        public List<TData> GetAll(string currentWorkPath)
        {
            string petsPath = currentWorkPath + "\\Data\\Pets";
            List<TData> datas = new List<TData>();
            DirectoryInfo[] petfolders = (new DirectoryInfo(petsPath)).GetDirectories();
            foreach (DirectoryInfo petfolder in petfolders)
            {
                foreach (FileInfo file in petfolder.GetFiles())
                {
                    if (file.Extension == ".json")
                    {
                        TData data = default(TData);
                        try
                        {
                            string jsonString = File.ReadAllText(file.FullName);
                            data = JsonSerializer.Deserialize<TData>(jsonString)!;
                        }
                        catch (Exception e)
                        {
                            System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                        }
                        if (data != null)
                        {
                            var imageSource = (data as Pet).ImageSource;
                            foreach (Moves move in Enum.GetValues(typeof(Moves)))
                            {
                                if(imageSource.ContainsKey(move) && imageSource[move] != null)
                                {
                                    imageSource[move] = $"{petfolder.FullName}\\" + imageSource[move];
                                }
                                else
                                {
                                    imageSource[move] = null;
                                }
                            }
                            datas.Add(data);
                        }
                    }
                }
            }
            return datas;

        }

        public void SavePetData(Pet pet, string currentWorkPath)
        {
            string petPath = currentWorkPath + $"\\Data\\Pets\\{pet.Name}";
            string jsonFile = petPath + $"\\{pet.Name}.json";
            string tempFile = petPath + $"\\{pet.Name}_tempdata.json";
            string backupPath = currentWorkPath + "\\Backups";
            string backupFile = backupPath + $"\\{pet.Name}_backupdata.json";

            if (!Directory.Exists(petPath))
            {
                Directory.CreateDirectory(petPath);
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
