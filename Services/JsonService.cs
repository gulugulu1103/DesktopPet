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
        private static readonly string BackupPath = Path.Combine(CurrentPath, "Backups");
        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            WriteIndented = true,
        };
        public List<Pet> GetAllPets()
        {
            return GetAllPets(Path.Combine(CurrentPath, "Data"));
        }
        public List<Pet> GetAllPets(string dataPath)
        {
            string petsPath = Path.Combine(dataPath, "Pets");
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
            SavePetData(pet, Path.Combine(CurrentPath, "Data"));
        }
        public void SavePetData(Pet pet, string dataPath)
        {
            string petsPath = Path.Combine(dataPath, "Pets");
            string petPath = Path.Combine(petsPath, $"{pet.Name}");
            string jsonFile = Path.Combine(petPath, $"{pet.Name}.json");
            string tempFile = Path.Combine(petPath, $"{pet.Name}_tempdata.json");
            string backupFile = Path.Combine(BackupPath, $"{pet.Name}_backupdata.json");
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
            GetSettings(ref SettingsHolder.settings, Path.Combine(CurrentPath + "Saves"));
        }
        public void GetSettings(string savePath)
        {
            GetSettings(ref SettingsHolder.settings, savePath);
        }
        public void GetSettings(ref DesktopPetSettings settings, string savePath)
        {
            string jsonFile = Path.Combine(savePath, $"Settings.json");
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
            SaveSettings(settings, Path.Combine(CurrentPath, "Saves"));
        }
        public void SaveSettings(DesktopPetSettings settings, string savePath)
        {
            // 检测savePath是否存在
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string jsonFile = Path.Combine(savePath, $"Settings.json");
            string tempFile = Path.Combine(savePath, $"Settings_tempdata.json");
            string backupFile = Path.Combine(BackupPath, $"Settings_backupdata.json");
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
                    imageSource[move] = Path.Combine(CurrentPath, $"Data\\Pets\\{Properties.Resources.SamplePetName}\\", imageSource[move]);
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
            string defaultPetPath = Path.Combine(Environment.CurrentDirectory, $"Data\\Pets\\{Properties.Resources.SamplePetName}");
            converter.ByteArrayToFile(Properties.Resources.SamplePet_Icon,     Path.Combine(defaultPetPath + "Icon.jpg"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_Stand,    Path.Combine(defaultPetPath + "Stand.gif"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_LieDown,  Path.Combine(defaultPetPath + "LieDown.gif"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveLeft, Path.Combine(defaultPetPath + "MoveLeft.gif"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveRight,Path.Combine(defaultPetPath + "MoveRight.gif"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveUp,   Path.Combine(defaultPetPath + "MoveUp.gif"));
            converter.ByteArrayToFile(Properties.Resources.SamplePet_MoveDown, Path.Combine(defaultPetPath + "MoveDown.gif"));
        }

        public JsonService()
        {
            List<string> paths = new()
            {
                BackupPath,
                Path.Combine(CurrentPath, "Data\\Pets"),
                Path.Combine(CurrentPath + "Saves"),
            };
            foreach(string path in paths)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

            }
        }
    }
}
