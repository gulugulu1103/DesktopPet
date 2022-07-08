using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    public class DesktopPetSettings
    {
        public Pet Pet { get; set; }

    }

    public static class SettingsHolder
    {
        public static DesktopPetSettings settings = new DesktopPetSettings();
    }
}
