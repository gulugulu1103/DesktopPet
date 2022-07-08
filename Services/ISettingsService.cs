using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface ISettingsService
    {
        void GetSettings();
        void SaveSettings();
    }
}
