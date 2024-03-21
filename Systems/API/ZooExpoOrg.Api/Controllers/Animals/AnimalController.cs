namespace ZooExpoOrg.Api.Controllers.Animals;

using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Logger;
using Asp.Versioning;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Animals")]
[Route("v{version:apiVersion}/animal")]
public class AnimalController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAnimalService animalService;

    public AnimalController(IAppLogger logger, IAnimalService animalService)
    {
        this.logger = logger;
        this.animalService = animalService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<AnimalModel>> Get()
    {
        var result = await animalService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await animalService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<AnimalModel> Create(CreateAnimalModel request)
    {
        var result = await animalService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateAnimalModel request)
    {
        await animalService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await animalService.Delete(id);
    }
}
