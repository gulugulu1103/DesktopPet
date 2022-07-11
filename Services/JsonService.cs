using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DesktopPet.Services
{
    public class JsonService : IPetService, ISettingsJsonService
    {
        private static readonly string CurrentPath = Environment.CurrentDirectory;
        private static readonly string BackupPath = CurrentPath + "\\Backups";
        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            WriteIndented = true,
        };
        public List<Pet> GetAllPets()
        {
            return GetAllPets(CurrentPath + "\\Data");
        }
        public List<Pet> GetAllPets(string dataPath)
        {
            string petsPath = dataPath + "\\Pets";
            List<Pet> pets = new List<Pet>();
            DirectoryInfo[] petfolders = (new DirectoryInfo(petsPath)).GetDirectories();
            foreach (DirectoryInfo petfolder in petfolders)
            {
                foreach (FileInfo file in petfolder.GetFiles())
                {
                    if (file.Extension == ".json")
                    {
                        Pet pet = null;
                        try
                        {
                            string jsonString = File.ReadAllText(file.FullName);
                            pet = JsonSerializer.Deserialize<Pet>(jsonString)!;
                        }
                        catch (Exception e)
                        {
                            System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                            Environment.Exit(0);
                        }
                        if (pet != null)
                        {
                            var imageSource = pet.ImageSource;
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
                            pets.Add(pet);
                        }
                    }
                }
            }
            return pets;

        }

        public void SavePetData(Pet pet)
        {
            SavePetData(pet, CurrentPath + "\\Data");
        }
        public void SavePetData(Pet pet, string dataPath)
        {
            string petsPath = dataPath + "\\Pets";
            string petPath = petsPath + $"\\{pet.Name}";
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
                File.WriteAllText(tempFile, JsonSerializer.Serialize(pet as SealedPet, jsonSerializerOptions));
                File.Replace(tempFile, jsonFile, backupFile);
            }
            else
            {
                File.WriteAllText(jsonFile, JsonSerializer.Serialize(pet, jsonSerializerOptions));
            }
        }

        public void GetSettings()
        {
            GetSettings(ref SettingsHolder.settings, CurrentPath + "\\Saves");
        }
        public void GetSettings(string savePath)
        {
            GetSettings(ref SettingsHolder.settings, savePath);
        }
        public void GetSettings(ref DesktopPetSettings settings, string savePath)
        {
            string jsonFile = savePath + $"\\Settings.json";
            try
            {
                string jsonString = File.ReadAllText(jsonFile);
                settings = JsonSerializer.Deserialize<DesktopPetSettings>(jsonString)!;
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    CreateDefaultSettings();
                }
                else
                {
                    System.Windows.MessageBox.Show("A handled exception just occurred: " + e.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    Environment.Exit(0);
                }
            }

        }

        public void SaveSettings()
        {
            SaveSettings(SettingsHolder.settings);
        }
        public void SaveSettings(DesktopPetSettings settings)
        {
            SaveSettings(settings, CurrentPath + "\\Saves");
        }
        public void SaveSettings(DesktopPetSettings settings, string savePath)
        {
            // 检测savePath是否存在
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string jsonFile = savePath + $"\\Settings.json";
            string tempFile = savePath + $"\\Settings_tempdata.json";
            string backupFile = BackupPath + $"\\Settings_backupdata.json";
            if (File.Exists(jsonFile))
            {
                File.WriteAllText(tempFile, JsonSerializer.Serialize(settings, jsonSerializerOptions));
                File.Replace(tempFile, jsonFile, backupFile);
            }
            else
            {
                File.WriteAllText(jsonFile, JsonSerializer.Serialize(settings, jsonSerializerOptions));
            }
        }

        public void CreateDefaultSettings()
        {
            DesktopPetSettings defaultSettings = new DesktopPetSettings()
            {
                Pet = new InitService().CreateSamplePet<Pet>(),
            };
            var imageSource = defaultSettings.Pet.ImageSource;
            foreach (Moves move in Enum.GetValues(typeof(Moves)))
            {
                if (imageSource.ContainsKey(move) && imageSource[move] != null)
                {
                    imageSource[move] = CurrentPath + $"\\Data\\Pets\\{Properties.Resources.SamplePetName}\\" + imageSource[move];
                }
                else
                {
                    imageSource[move] = null;
                }
            }

            SaveSettings(defaultSettings);
        }
        public void CreateDefaultPetData()
        {
            SavePetData(new InitService().CreateSamplePet<Pet>());
            ByteToFileConverter converter = new ByteToFileConverter();
            string defaultPetPath = Environment.CurrentDirectory + $"\\Data\\Pets\\{Properties.Resources.SamplePetName}";
            converter.ByteArrayToFile(Properties.Resources.SamplePet_Icon, defaultPetPath + "\\Icon.jpg");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_Stand, defaultPetPath + "\\Stand.gif");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_LieDown, defaultPetPath + "\\LieDown.gif");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveLeft, defaultPetPath + "\\MoveLeft.gif");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveRight, defaultPetPath + "\\MoveRight.gif");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveUp, defaultPetPath + "\\MoveUp.gif");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveDown, defaultPetPath + "\\MoveDown.gif");
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
            if (!Directory.Exists(CurrentPath + "\\Saves"))
            {
                Directory.CreateDirectory(CurrentPath + "\\Saves");
            }
        }
    }
}
