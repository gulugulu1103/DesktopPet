using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface IJsonService<TData>
    {
        // 在path中存储单个pet的
        void SavePetData(Pet data, string currentWorkPath);
        List<TData> GetAll(string currentWorkPath);
    }
}
