namespace dotnet_rpg.Dtos.Character {

    public class AddWeaponDto {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Guid CharacterId { get; set; }
    }
}