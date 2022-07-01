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
    public class JsonService<TData> : IJsonServices<TData>
    {
        public static readonly string DataPath = System.Environment.CurrentDirectory + "\\Data";
        public static readonly string SavePath = DataPath + "\\Saves";
        public static readonly string PetPath = DataPath + "\\Pets";
        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        public List<TData> GetAll(string path)
        {
            List<TData> datas = new List<TData>();
            DirectoryInfo folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles())
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
                        datas.Add(data);
                }
            }
            return datas;

        }

        public void SavePetData(PetData petData, string path)
        {
            string jsonFile = path + $"\\{petData.Name}_data.json";
            string tempFile = path + $"\\{petData.Name}_tempdata.json";
            string backupPath = path + "\\Backups";
            string backupFile = backupPath + $"\\{petData.Name}_backupdata.json";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            if (File.Exists(jsonFile))
            {
                File.WriteAllText(tempFile, JsonSerializer.Serialize(petData, jsonSerializerOptions));
                File.Replace(tempFile, jsonFile, backupFile);
            }
            else
            {
                File.WriteAllText(jsonFile, JsonSerializer.Serialize(petData, jsonSerializerOptions));
            }
        }
    }
}
