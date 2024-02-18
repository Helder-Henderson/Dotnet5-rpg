using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Dtos.Character {

    public class GetWeaponDto {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
    }
}