using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    public class PetData : Pet
    {
        public Status Status { get; set; }
        public DateTime CreateTime { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Happy { get; set; }

        public PetData(Pet pet)
        {
            this.Status = Status.Normal;
            this.CreateTime = DateTime.Now;
            this.Health = pet.MaxHealth;
            this.Hunger = pet.MaxHunger;
            this.Happy = 0;
        }
    }
}
