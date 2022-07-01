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
    public class JsonDataService
    {
        public static readonly string DataPath = System.Environment.CurrentDirectory + "\\Data";

        public static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
    }
}
