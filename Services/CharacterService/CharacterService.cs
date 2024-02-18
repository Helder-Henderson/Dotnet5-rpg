using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Interfaces.CharacterService;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper Mapper;
    private readonly DataContext Context;
    private readonly IHttpContextAccessor HttpContextAccessor;

    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        Mapper = mapper;
        Context = context;
        HttpContextAccessor = httpContextAccessor;
    }

    private Guid GetUserId() => Guid.Parse(HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = Mapper.Map<Character>(newCharacter);
        character.User = await Context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

        Context.Characters.Add(Mapper.Map<Character>(character));

        await Context.SaveChangesAsync();

        serviceResponse.Data = await Context.Characters
        .Where(c => c.User.Id == GetUserId())
        .Select(c => Mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var userId = GetUserId();
        var response = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await Context.Characters
        .Include(c => c.Weapon)
        .Include(_ => _.Skills)
        .Where(c => c.User.Id == userId)
        .ToListAsync();

        response.Data = dbCharacters
                        .Select(c => Mapper.Map<GetCharacterDto>(c))
                        .ToList();
        return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter = await Context.Characters
        .Include(c=> c.Skills)
        .Include(_=> _.Weapon)
        .FirstOrDefaultAsync(_ => _.Id == id && _.User.Id == GetUserId());
        serviceResponse.Data = Mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto?>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {
            Character? character = await Context.Characters
                                    .Include(c => c.User)
                                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

            if (character.User.Id == GetUserId())
            {
                Mapper.Map(updatedCharacter, character);
                serviceResponse.Data = Mapper.Map<GetCharacterDto>(character);
                await Context.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }



        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(Guid id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

        try
        {
            Character? character = await Context.Characters
            .FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

            if (character != null)
            {
                Context.Characters.Remove(character);
                await Context.SaveChangesAsync();

                serviceResponse.Data = Context.Characters
                                        .Where(c => c.User.Id == GetUserId())
                                        .Select(c => Mapper.Map<GetCharacterDto>(c))
                                        .ToList();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character = await Context.Characters
                            .Include(c => c.Weapon)
                            .Include(c => c.Skills)
                            .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId
                                                    && c.User.Id == GetUserId());

            if (character is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            var skill = await Context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
            if(skill is null) {
                response.Success = false;
                response.Message = "Skill not found";
                return response;
            }

            character.Skills.Add(skill);
            await Context.SaveChangesAsync();
            response.Data = Mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }
        return response;
    }
}

