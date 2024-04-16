using Asp.Versioning;
using AutoMapper;
using Azure.Core;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Expositions;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Api.Controllers.Expositions;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Expositions")]
[Route("v{version:apiVersion}/exposition")]
public class ExpositionController : Controller
{
    private readonly IAppLogger logger;
    private readonly IExpositionService expositionService;
    private readonly IMapper mapper;

    public ExpositionController(
        IAppLogger logger,
        IExpositionService expositionService, 
        IMapper mapper
        )
    {
        this.logger = logger;
        this.expositionService = expositionService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var expositions = await expositionService.GetAll();

        if (expositions.IsNullOrEmpty())
            return NotFound("Expositions not found.");

        return Ok(mapper.Map<ICollection<PresintationExpositionModel>>(expositions));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var exposition = await expositionService.GetById(id);

        if (exposition == null)
            return NotFound($"Exposition (ID = {id}) not found.");

        return Ok(mapper.Map<PresintationExpositionModel>(exposition));
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateExpositionModel model)
    {
        try
        {
            var exposition = await expositionService.Create(model);

            return Ok(mapper.Map<PresintationExpositionModel>(exposition));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExpositionModel model)
    {
        try
        {
            await expositionService.Update(id, model);

            return Ok();
        }
        catch(ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/subscribe/{clientId:Guid}")]
    public async Task<IActionResult> Subscribe(Guid id, Guid clientId)
    {
        try
        {
            await expositionService.Subscribe(id, clientId);

            return Ok();
        }
        catch (ProcessException e)
        {
            if (e.Message == $"Client (ID = {clientId}) is the organizer")
            {
                return BadRequest(e.Message);
            }

            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/unsubscribe/{clientId:Guid}")]
    public async Task<IActionResult> Unsubscribe(Guid id, Guid clientId)
    {
        try
        {
            await expositionService.Unsubscribe(id, clientId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/participants/add/{animalId:Guid}")]
    public async Task<IActionResult> AddParticipant(Guid id, Guid animalId)
    {
        try
        {
            await expositionService.AddParticipant(id, animalId);

            return Ok();
        }
        catch (ProcessException e)
        {
            if (e.Message == $"Animal type does not correspond to the type of exposition participants")
            {
                return BadRequest(e.Message);
            }

            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/participants/delete/{animalId:Guid}")]
    public async Task<IActionResult> DeleteParticipant(Guid id, Guid animalId)
    {
        try
        {
            await expositionService.DeleteParticipant(id, animalId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await expositionService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound();
        }
    }
}
