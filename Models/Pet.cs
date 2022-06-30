﻿namespace DesktopPet.Models
{
    public class Pet
    {
        public string Name { get; set; } = "Unknown";
        public string Description { get; set; } = "No Description Here";
        public double Health { get; set; } = 100;
        public double MaxHealth { get; set; } = 100;
        public double Hunger { get; set; } = 100;
        public double MaxHunger { get; set; } = 100;
        public double Happy { get; set; } = 100;
        public double MaxHappy { get; set; } = 100;
        public string ImageSource { get; set; } = @"\Views\Resources\Icons\Icon.png";
    }
}
