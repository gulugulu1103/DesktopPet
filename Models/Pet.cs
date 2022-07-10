using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopPet.Models
{
    public class Pet
    {
        public string Name { get; set; } = "Unknown";
        public string Description { get; set; } = "No Description Here";
        // public string Icon { get; set; } = "Icon.jpg";
        public Dictionary<Moves, string?> ImageSource { get; set; } = new Dictionary<Moves, string?>();
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name = {this.Name}");
            stringBuilder.AppendLine($"Description = {this.Description}");
            stringBuilder.AppendLine($"ImageSource:");
            foreach (var item in this.ImageSource)
            {
                stringBuilder.AppendLine($"{item.Key} = {item.Value}");
            }
            return stringBuilder.ToString();
        }
        public Pet()
        {
            foreach (Moves move in Enum.GetValues(typeof(Moves)))
            {
                this.ImageSource.TryAdd(move, null);
            }
        }
    }
}
