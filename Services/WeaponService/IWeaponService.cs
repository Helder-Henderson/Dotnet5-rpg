using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.Interfaces.CharacterService;

public interface IWeaponService{
       Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
       //Task<ServiceResponse<List<GetCharacterDto>>> DeleteWeapon(AddCharacterDto newCharacter);
}

