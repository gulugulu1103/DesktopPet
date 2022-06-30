using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface IPetDataServices
    {
        // 该接口主要读取和生成保存每个宠物信息的pets.json
        void SaveAll(List<Pet> pets);
        void SavePet(Pet pet);
        Pet GetPet(string Name);
    }
}
