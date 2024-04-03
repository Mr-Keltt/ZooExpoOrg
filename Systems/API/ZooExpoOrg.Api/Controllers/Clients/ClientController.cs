using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Clients;

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
    public async Task<IEnumerable<ClientModel>> Get()
    {
        var result = await clientService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await clientService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<IEnumerable<PresintationClientModel>>(result));
    }

    [HttpPost("")]
    public async Task<PresintationClientModel> Create(CreateClientModel request)
    {
        var result = await clientService.Create(request);

        return mapper.Map<PresintationClientModel>(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateClientModel request)
    {
        await clientService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await clientService.Delete(id);
    }
}
