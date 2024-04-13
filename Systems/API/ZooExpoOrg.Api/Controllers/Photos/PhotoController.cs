namespace ZooExpoOrg.Api.Controllers.Photos;

using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Common.Exceptions;
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

    [HttpGet("owned/{ownerId:Guid}")]
    public async Task<IActionResult> GetAllOwnedById([FromRoute] Guid ownerId)
    {
        var result = await photoService.GetAllOwnedById(ownerId);

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
    public async Task<IActionResult> Create(CreatePhotoModel createModel)
    {
        try
        {
            var result = await photoService.Create(createModel);

            return Ok(mapper.Map<PresintationPhotoModel>(result));
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
            await photoService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
