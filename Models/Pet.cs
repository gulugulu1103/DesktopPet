using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopPet.Models
{
    public class Pet
    {
        public string Name { get; set; } = "Unknown";
        public string Description { get; set; } = "No Description Here";
        public double Health { get; set; } = 100;
        public double MaxHealth { get; set; } = 100;
        public double Hunger { get; set; } = 100;
        public double MaxHunger { get; set;} = 100;
        public double Happy { get; set; } = 100;
        public double MaxHappy { get; set; } = 100;
        public string ImageSource { get; set; } = "/Resources/Icon.png";
    }
}
