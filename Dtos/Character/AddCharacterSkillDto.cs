using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Dtos.Character {

    public class AddCharacterSkillDto {
        public Guid CharacterId { get; set; }
        public Guid SkillId { get; set; }
    }
}