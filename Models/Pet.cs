using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopPet.Models
{
    [MessagePack.MessagePackObject]
    public class Pet
    {
        [MessagePack.Key(0)]
        public string Name { get; set; } = "Unknown";
        [MessagePack.Key(1)]
        public string Description { get; set; } = "No Description Here";
        [MessagePack.Key(2)]
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
