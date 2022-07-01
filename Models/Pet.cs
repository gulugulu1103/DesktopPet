using System.Collections.Generic;

namespace DesktopPet.Models
{
    public class Pet
    {
        public string Name { get; set; } = "Unknown";
        public string Description { get; set; } = "No Description Here";
        public double MaxHealth { get; set; } = 100;
        public double MaxHunger { get; set; } = 100;
        public double MaxHappy { get; set; } = 100;
        public double MinHappy { get; set; } = -100;
        public Dictionary<Status, string?> ImageSource { get; set; } = new Dictionary<Status, string?>();
    }
}
