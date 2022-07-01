using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface IJsonServices<TData>
    {
        void SavePetData(PetData data, string path);
        List<TData> GetAll(string path);
    }
}
