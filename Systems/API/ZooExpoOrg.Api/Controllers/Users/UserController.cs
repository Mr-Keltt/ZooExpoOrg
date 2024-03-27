using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Users;

namespace ZooExpoOrg.Api.Controllers.Users;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Users")]
[Route("v{version:apiVersion}/user")]
public class UserController : Controller
{
    private readonly IAppLogger logger;
    private readonly IUserService userService;
    private readonly IMapper mapper;

    public UserController(IAppLogger logger, IUserService userService, IMapper mapper)
    {
        this.logger = logger;
        this.userService = userService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<UserModel>> Get()
    {
        var result = await userService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await userService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<IEnumerable<PresintationUserModel>>(result));
    }

    [HttpPost("")]
    public async Task<PresintationUserModel> Create(CreateUserModel request)
    {
        var result = await userService.Create(request);

        return mapper.Map<PresintationUserModel>(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateUserModel request)
    {
        await userService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await userService.Delete(id);
    }
}
