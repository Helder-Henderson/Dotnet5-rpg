using System.Text.Json.Serialization;
using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Models {

    public class Character {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public ERpgClass Class { get; set; } = ERpgClass.Mage;
        public User? User { get; set; }
        public Weapon? Weapon { get; set; }
        public List<Skill> Skills { get; set; }
    }
}