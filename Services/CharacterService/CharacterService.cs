using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Interfaces.CharacterService;

namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    public IMapper ServiceMapper { get; set; }
    public List<Character> Characters { get; set; }

    public CharacterService(IMapper mapper) {
        Characters = new List<Character> {
            new() {
                Name = "Herbert",
                Class = Models.Enums.ERpgClass.Knight
            },
            new() {
                Name = "Helder",
                Class = Models.Enums.ERpgClass.Cleric
            } 
        };
        ServiceMapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        
        Characters.Add(ServiceMapper.Map<Character>(newCharacter));

        serviceResponse.Data = Characters.Select(c => ServiceMapper.Map<GetCharacterDto>(c)).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {    
        return new ServiceResponse<List<GetCharacterDto>> {
            Data = ServiceMapper.Map<List<GetCharacterDto>>(Characters)
        };
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var character = Characters.FirstOrDefault(_ => _.Id == id, null);
        serviceResponse.Data = ServiceMapper.Map<GetCharacterDto>(character);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto?>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try {
            Character? character = Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id, null);
            ServiceMapper.Map(updatedCharacter, character);

            serviceResponse.Data = ServiceMapper.Map<GetCharacterDto>(character);

        } catch (Exception ex) {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(Guid id)
    {
       var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try {
            Character? character = Characters.First(c => c.Id == id);
            Characters.Remove(character);
            serviceResponse.Data = ServiceMapper.Map<GetCharacterDto>(character);

        } catch (Exception ex) {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

}

