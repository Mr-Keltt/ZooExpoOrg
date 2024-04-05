using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Clients;
using Microsoft.IdentityModel.Tokens;

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
            return NotFound();

        return Ok(mapper.Map<IEnumerable<PresintationClientModel>>(clients));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await clientService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<PresintationClientModel>(result));
    }

    [HttpPost("")]
    public async Task<PresintationClientModel> Create(PresintationCreateClientModel request)
    {
        var result = await clientService.Create(mapper.Map<CreateClientModel>(request));

        return mapper.Map<PresintationClientModel>(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, PresintationUpdateClientModel request)
    {
        await clientService.Update(id, mapper.Map<UpdateClientModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await clientService.Delete(id);
    }
}
