using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Clients;
using Microsoft.IdentityModel.Tokens;
using ZooExpoOrg.Common.Exceptions;
using Azure.Core;

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
            return NotFound("Clients not found.");

        return Ok(mapper.Map<PresintationClientModel>(result));
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateClientModel request)
    {
        try
        {
            var result = await clientService.Create(request);

            return Ok(mapper.Map<PresintationClientModel>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateClientModel model)
    {
        try
        {
            await clientService.Update(id, model);

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
            await clientService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
