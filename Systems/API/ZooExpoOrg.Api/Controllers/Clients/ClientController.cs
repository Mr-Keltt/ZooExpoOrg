using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Clients;
using Microsoft.IdentityModel.Tokens;
using ZooExpoOrg.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Services.RightVerifier;
using ZooExpoOrg.Services.ExpositionsNotificationManager;

namespace ZooExpoOrg.Api.Controllers.Clients;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Clients")]
[Route("v{version:apiVersion}/client")]
public class ClientController : Controller
{
    private readonly IAppLogger logger;
    private readonly IClientService clientService;
    private readonly IMapper mapper;
    private readonly IRightVerifierService rightVerifier;
    private readonly IExpositionsNotificationManagerService expositionsNotificationManager;

    public ClientController(
        IAppLogger logger, 
        IClientService clientService, 
        IMapper mapper,
        IRightVerifierService rightVerifier,
        IExpositionsNotificationManagerService expositionsNotificationManager
        )
    {
        this.logger = logger;
        this.clientService = clientService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
        this.expositionsNotificationManager = expositionsNotificationManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var clients = await clientService.GetAll();

        if (clients.IsNullOrEmpty())
            return NotFound("Clients not found.");

        return Ok(mapper.Map<IEnumerable<PresintationClientModel>>(clients));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await clientService.GetById(id);

        if (result == null)
            return NotFound($"Clients (ID = {id}) not found.");

        return Ok(mapper.Map<PresintationClientModel>(result));
    }

    [HttpPost("")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Create(PresintationCreateClientModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfCreateClient(jwtToken, model.UserId)))
            {
                return BadRequest("Access denied.");
            }

            var result = await clientService.Create(mapper.Map<CreateClientModel>(model));

            return Ok(mapper.Map<PresintationClientModel>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Update([FromRoute] Guid id, PresintationUpdateClientModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagClient(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await clientService.Update(id, mapper.Map<UpdateClientModel>(model));

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}/notification/{notificationId:Guid}/markreader")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> NotificationMarkReader(Guid id, Guid notificationId)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagClient(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await expositionsNotificationManager.MarkNotificationReaderByClientId(id, notificationId);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagClient(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await clientService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
