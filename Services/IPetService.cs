using DesktopPet.Models;
using System.Collections.Generic;

namespace DesktopPet.Services
{
    interface IPetService<TData>
    {
        // 在path中存储单个pet的
        void SavePetData(Pet data, string currentWorkPath);
        List<TData> GetAll(string currentWorkPath);
    }
}
