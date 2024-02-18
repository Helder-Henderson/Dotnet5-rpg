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
public class CharacterController : ControllerBase
{
    private readonly ILogger<CharacterController> Logger;
    private readonly ICharacterService CharacterService;

    public CharacterController(ICharacterService characterService,ILogger<CharacterController> logger)
    {
        Logger = logger;
        CharacterService = characterService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAll()
    {
        return Ok(await CharacterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetOne(Guid id)
    {
        var response = await CharacterService.GetCharacterById(id);
        if(response.Data is null) return NotFound();
        return Ok(response);
    }

    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Delete(Guid id)
    {
        var response = await CharacterService.DeleteCharacter(id);
        if(response.Data is null) return BadRequest(response);
        return Ok(response);
    }  

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> Create(AddCharacterDto newCharacter)
    {
        var response = await CharacterService.AddCharacter(newCharacter);
        if(response.Data is null) return BadRequest();
        return Ok(response);
    }   

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        return Ok(await CharacterService.AddCharacterSkill(newCharacterSkill));
    } 
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Update(UpdateCharacterDto updateCharacter)
    {
        var response = await CharacterService.UpdateCharacter(updateCharacter);
        if(response.Data is null) return NotFound(response);
        return Ok(response);
    }    

}

