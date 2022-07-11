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
