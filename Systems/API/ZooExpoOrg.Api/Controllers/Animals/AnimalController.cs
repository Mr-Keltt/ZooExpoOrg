namespace ZooExpoOrg.Api.Controllers.Animals.Animals;

using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Logger;
using Asp.Versioning;
using AutoMapper;
using ZooExpoOrg.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Animals")]
[Route("v{version:apiVersion}/animal")]
public class AnimalController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAnimalService animalService;
    private readonly IMapper mapper;

    public AnimalController(IAppLogger logger, IAnimalService animalService, IMapper mapper)
    {
        this.logger = logger;
        this.animalService = animalService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var result = await animalService.GetAll();

        if (result.IsNullOrEmpty())
        {
            return NotFound("Animals not found.");
        }

        return Ok(mapper.Map<IEnumerable<PresintationAnimalModel>>(result));
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
    public async Task<IActionResult> Create(CreateAnimalModel request)
    {
        try
        {
            var result = await animalService.Create(request);

            return Ok(mapper.Map<PresintationAnimalModel>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateAnimalModel request)
    {
        try
        {
            await animalService.Update(id, request);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await animalService.Delete(id);
            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
