using System.Security.Claims;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Interfaces.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WeaponController : ControllerBase
{
    private readonly ILogger<WeaponController> Logger;
    private readonly IWeaponService WeaponService;

    public WeaponController(IWeaponService weaponService,ILogger<WeaponController> logger)
    {
        Logger = logger;
        WeaponService = weaponService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon) {
        return Ok(await WeaponService.AddWeapon(newWeapon));
    }

}

