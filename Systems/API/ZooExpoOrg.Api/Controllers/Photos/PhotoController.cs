namespace ZooExpoOrg.Api.Controllers.Photos;

using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Photos;
using ZooExpoOrg.Services.RightVerifier;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Technical")]
[Route("v{version:apiVersion}/photo")]
public class PhotoController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IPhotoService photoService;
    private readonly IMapper mapper;
    private readonly IRightVerifierService rightVerifier;

    public PhotoController(
        IAppLogger logger, 
        IPhotoService photoService, 
        IMapper mapper,
        IRightVerifierService rightVerifier)
    {
        this.logger = logger;
        this.photoService = photoService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
    }

    [HttpGet("located/{locationId:Guid}")]
    public async Task<IActionResult> GetAllLocatedById([FromRoute] Guid locationId)
    {
        var result = await photoService.GetAllLocationById(locationId);

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
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Create(CreatePhotoModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfCreatePhoto(jwtToken, model.OwnerId, model.LocationId)))
            {
                return BadRequest("Access denied.");
            }

            var result = await photoService.Create(model);

            return Ok(mapper.Map<PresintationPhotoModel>(result));
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

            if (!(await rightVerifier.VerifRightsOfManagPhoto(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await photoService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
