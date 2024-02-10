using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Dtos.Character {

    public class GetCharacterDto {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public ERpgClass Class { get; set; } = ERpgClass.Mage;
    }
}