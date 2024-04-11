namespace ZooExpoOrg.Api.Controllers.Photos;

using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Photos;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Technical")]
[Route("v{version:apiVersion}/photo")]
public class PhotoController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IPhotoService photoService;
    private readonly IMapper mapper;

    public PhotoController(IAppLogger logger, IPhotoService photoService, IMapper mapper)
    {
        this.logger = logger;
        this.photoService = photoService;
        this.mapper = mapper;
    }

    [HttpGet("owned/{ownedId:Guid}")]
    public async Task<IActionResult> GetAllOwnedById([FromRoute] Guid ownedId)
    {
        var result = await photoService.GetAllOwnedById(ownedId);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<IEnumerable<PresintationPhotoModel>>(result));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await photoService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<PresintationPhotoModel>(result));
    }

    [HttpPost("")]
    public async Task<PresintationPhotoModel> Create(PresintationCreatePhotoModel createModel)
    {
        var result = await photoService.Create(mapper.Map<CreatePhotoModel>(createModel));

        return mapper.Map<PresintationPhotoModel>(result);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await photoService.Delete(id);
    }
}
