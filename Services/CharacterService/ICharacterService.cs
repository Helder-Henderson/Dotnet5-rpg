using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.Interfaces.CharacterService;

public interface ICharacterService{
       Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
       Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
       Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(Guid id);
       Task<ServiceResponse<GetCharacterDto?>> GetCharacterById(Guid id);
       Task<ServiceResponse<GetCharacterDto?>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
}

