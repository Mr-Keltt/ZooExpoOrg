using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Api.Controllers.Expositions.Models;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Expositions;
using ZooExpoOrg.Services.ExpositionsNotificationManager;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.RightVerifier;


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
    private readonly IRightVerifierService rightVerifier;
    private readonly IExpositionsNotificationManagerService expositionsNotificationManager;

    public ExpositionController(
        IAppLogger logger,
        IExpositionService expositionService, 
        IMapper mapper,
        IRightVerifierService rightVerifier,
        IExpositionsNotificationManagerService expositionsNotificationManager
        )
    {
        this.logger = logger;
        this.expositionService = expositionService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
        this.expositionsNotificationManager = expositionsNotificationManager;
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
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Create(PresintationCreateExpositionModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfCreateExposition(jwtToken, model.OrganizerId)))
            {
                return BadRequest("Access denied.");
            }

            var exposition = await expositionService.Create(mapper.Map<CreateExpositionModel>(model));

            return Ok(mapper.Map<PresintationExpositionModel>(exposition));
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
    public async Task<IActionResult> Update(Guid id, PresintationUpdateExpositionModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagExposition(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await expositionService.Update(id, mapper.Map<UpdateExpositionModel>(model));

            return Ok();
        }
        catch(ProcessException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpPut("{id:Guid}/notification/send")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> SendNotification(Guid id, PresintationCreateNotificationModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfCreateNotification(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await expositionsNotificationManager.SendNotification(id, mapper.Map<CreateNotificationModel>(model));

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("notification/cancel/{notificationId:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> cancelMailing(Guid notificationId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagNotification(jwtToken, notificationId)))
            {
                return BadRequest("Access denied.");
            }

            await expositionsNotificationManager.CancelMailingByNotificationId(notificationId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/subscribe/{clientId:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Subscribe(Guid id, Guid clientId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagClient(jwtToken, clientId)))
            {
                return BadRequest("Access denied.");
            }

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
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Unsubscribe(Guid id, Guid clientId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagClient(jwtToken, clientId)))
            {
                if (!(await rightVerifier.VerifRightsOfManagExposition(jwtToken, id)))
                {
                    return BadRequest("Access denied.");
                }
            }

            await expositionService.Unsubscribe(id, clientId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/participants/add/{animalId:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> AddParticipant(Guid id, Guid animalId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagAnimal(jwtToken, animalId)))
            {
                return BadRequest("Access denied.");
            }

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
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> DeleteParticipant(Guid id, Guid animalId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagAnimal(jwtToken, animalId)))
            {
                if (!(await rightVerifier.VerifRightsOfManagExposition(jwtToken, id)))
                {
                    return BadRequest("Access denied.");
                }
            }

            await expositionService.DeleteParticipant(id, animalId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagExposition(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await expositionService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound();
        }
    }
}
