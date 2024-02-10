using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Dtos.Character {

    public class UpdateCharacterDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public ERpgClass Class { get; set; } = ERpgClass.Mage;
    }
}