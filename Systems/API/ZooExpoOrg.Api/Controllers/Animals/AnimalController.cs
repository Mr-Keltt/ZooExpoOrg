namespace ZooExpoOrg.Api.Controllers.Animals.Animals;

using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Logger;
using Asp.Versioning;
using AutoMapper;
using ZooExpoOrg.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Services.RightVerifier;
using FluentValidation;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Animals")]
[Route("v{version:apiVersion}/animal")]
public class AnimalController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAnimalService animalService;
    private readonly IMapper mapper;
    private readonly IRightVerifierService rightVerifier;

    public AnimalController(
        IAppLogger logger, 
        IAnimalService animalService, 
        IMapper mapper,
        IRightVerifierService rightVerifier
        )
    {
        this.logger = logger;
        this.animalService = animalService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await animalService.GetById(id);

        if (result == null)
        {
            return NotFound($"Animal (ID = {id}) not found.");
        }

        return Ok(mapper.Map<PresintationAnimalModel>(result));
    }

    [HttpGet("owned/{ownerId:Guid}")]
    public async Task<IActionResult> GetOwned([FromRoute] Guid ownerId)
    {
        try
        {
            var result = await animalService.GetOwned(ownerId);

            if (result.IsNullOrEmpty())
            {
                return NotFound("Animals not found.");
            }

            return Ok(mapper.Map<IEnumerable<PresintationAnimalModel>>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Create(PresintationCreateAnimalModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");


            if (!(await rightVerifier.VerifRightsOfCreateAnimal(jwtToken, model.OwnerId)))
            {
                return BadRequest("Access denied.");
            }

            var result = await animalService.Create(mapper.Map<CreateAnimalModel>(model));

            return Ok(mapper.Map<PresintationAnimalModel>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Update([FromRoute] Guid id, PresintationUpdateAnimalModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagAnimal(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await animalService.Update(id, mapper.Map<UpdateAnimalModel>(model));

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagAnimal(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await animalService.Delete(id);
            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
