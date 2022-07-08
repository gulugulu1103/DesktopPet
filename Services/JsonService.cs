using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace DesktopPet.Services
{
    public class JsonService<TData> : IPetService<TData>, ISettingsService
    {
        private static readonly string CurrentPath = Environment.CurrentDirectory;
        private static readonly string BackupPath = CurrentPath + "\\Backups";
        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            WriteIndented = true,
        };
        public List<TData> GetAll(string dataPath)
        {
            string petsPath = dataPath + "\\Pets";
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
                                if (imageSource.ContainsKey(move) && imageSource[move] != null)
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

        public void SavePetData(Pet pet, string dataPath)
        {
            string petPath = dataPath + $"\\{pet.Name}";
            string jsonFile = petPath + $"\\{pet.Name}.json";
            string tempFile = petPath + $"\\{pet.Name}_tempdata.json";
            string backupFile = BackupPath + $"\\{pet.Name}_backupdata.json";
            if (!Directory.Exists(petPath))
            {
                Directory.CreateDirectory(petPath);
            }
            var imageSource = pet.ImageSource;
            foreach (Moves move in Enum.GetValues(typeof(Moves)))
            {
                if (imageSource.ContainsKey(move) && imageSource[move] != null)
                {
                    imageSource[move] = imageSource[move].Split('\\').Last();
                }
                else
                {
                    imageSource[move] = null;
                }
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

        public void GetSettings()
        {
            string savesPath = CurrentPath + "\\Saves";
            string jsonFile = savesPath + $"\\Settings.json";
            try
            {
                string jsonString = File.ReadAllText(jsonFile);
                SettingsHolder.settings = JsonSerializer.Deserialize<DesktopPetSettings>(jsonString)!;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
        }

        public void SaveSettings()
        {
            string savesPath = CurrentPath + "\\Saves";
            string jsonFile = savesPath + $"\\Settings.json";
            string tempFile = savesPath + $"\\Settings_tempdata.json";
            string backupFile = BackupPath + $"\\Settings_backupdata.json";
            if (File.Exists(jsonFile))
            {
                File.WriteAllText(tempFile, JsonSerializer.Serialize(SettingsHolder.settings, jsonSerializerOptions));
                File.Replace(tempFile, jsonFile, backupFile);
            }
            else
            {
                File.WriteAllText(jsonFile, JsonSerializer.Serialize(SettingsHolder.settings, jsonSerializerOptions));
            }
        }

        public JsonService()
        {
            if (!Directory.Exists(BackupPath))
            {
                Directory.CreateDirectory(BackupPath);
            }
            if (!Directory.Exists(CurrentPath + "\\Data\\Pets"))
            {
                Directory.CreateDirectory(CurrentPath + "\\Data\\Pets");
            }
            if(!Directory.Exists(CurrentPath + "\\Saves")){
                Directory.CreateDirectory(CurrentPath + "\\Saves");
            }
        }
    }
}
