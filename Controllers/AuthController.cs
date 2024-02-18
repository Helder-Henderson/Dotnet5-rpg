using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Interfaces.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<CharacterController> Logger;
    private readonly IAuthRepository AuthRepository;

    public AuthController(IAuthRepository authRepository,ILogger<CharacterController> logger)
    {
        Logger = logger;
        AuthRepository = authRepository;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<Guid>>> Register(UserRegisterDto request) {
        var response = await AuthRepository.Register(
            new User {Username = request.Username },
            request.Password
        );
        if(!response.Success) {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<Guid>>> Login(UserLoginDto request) {
        var response = await AuthRepository.Login(request.Username, request.Password);
        if(!response.Success) {
            return BadRequest(response);
        }
        return Ok(response);
    }
}

