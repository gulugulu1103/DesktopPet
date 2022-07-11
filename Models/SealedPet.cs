using System.Collections.Generic;

namespace DesktopPet.Models
{
    [MessagePack.MessagePackObject]
    public class SealedPet : Pet
    {
        [MessagePack.Key(3)]
        public Dictionary<Moves, byte[]?> ImageBytes { get; set; } = new Dictionary<Moves, byte[]?>();

        [MessagePack.Key(4)]
        private readonly bool MySealed = true;
    }
}
