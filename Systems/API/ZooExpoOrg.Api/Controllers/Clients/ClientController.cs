using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Clients;
using Microsoft.IdentityModel.Tokens;
using ZooExpoOrg.Common.Exceptions;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using ZooExpoOrg.Common.Security;

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

    public ClientController(IAppLogger logger, IClientService clientService, IMapper mapper)
    {
        this.logger = logger;
        this.clientService = clientService;
        this.mapper = mapper;
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
            await clientService.Update(id, mapper.Map<UpdateClientModel>(model));

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
            await clientService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
