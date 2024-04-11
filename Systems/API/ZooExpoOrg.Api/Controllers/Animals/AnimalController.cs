namespace ZooExpoOrg.Api.Controllers.Animals;

using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Logger;
using Asp.Versioning;
using AutoMapper;

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
    public async Task<IEnumerable<PresintationAnimalModel>> Get()
    {
        var result = await animalService.GetAll();

        return mapper.Map<IEnumerable<PresintationAnimalModel>>(result);
    }

    [HttpGet("owned/{ownerId:Guid}")]
    public async Task<IActionResult> GetOwned([FromRoute] Guid ownerId)
    {
        var result = await animalService.GetOwned(ownerId);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<IEnumerable<PresintationAnimalModel>>(result));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await animalService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<PresintationAnimalModel>(result));
    }

    [HttpPost("")]
    public async Task<PresintationAnimalModel> Create(CreateAnimalModel request)
    {
        var result = await animalService.Create(request);

        return mapper.Map<PresintationAnimalModel>(result);
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
