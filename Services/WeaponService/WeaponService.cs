using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Interfaces.CharacterService;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService;

public class WeaponService : IWeaponService
{
    private readonly IMapper Mapper;
    private readonly DataContext Context;
    private readonly IHttpContextAccessor HttpContextAccessor;

    public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        Mapper = mapper;
        Context = context;
        HttpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        try 
        {
            Character character = await Context.Characters
                                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                                    c.User.Id == Guid.Parse(HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if(character is null) {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            Weapon weapon = new Weapon {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };

            Context.Weapons.Add(weapon);
            await Context.SaveChangesAsync();

            response.Data = Mapper.Map<GetCharacterDto>(character);
        } 
        catch(Exception err) {
            response.Success = false;
            response.Message = err.Message;
        }
        return response;    
    }
}

