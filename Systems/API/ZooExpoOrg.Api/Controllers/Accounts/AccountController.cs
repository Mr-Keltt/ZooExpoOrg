namespace ZooExpoOrg.Api.Controllers.Accounts;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using ZooExpoOrg.Services.Accounts;
using ZooExpoOrg.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Api.Controllers.Achievements;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Account")]
[Route("v{version:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountController> logger;
    private readonly IAccountService accountService;

    public AccountController(IMapper mapper, ILogger<AccountController> logger, IAccountService accountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.accountService = accountService;
    }

    [HttpGet("")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Register()
    {
        try
        {
            var users = await accountService.GetAll();

            if (users == null)
            {
                return NotFound($"Users not found.");
            }

            return Ok(mapper.Map<IEnumerable<PresintationAccountModel>>(users));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> Register(PresintationRegisterAccountModel request)
    {
        try
        {
            var user = await accountService.Create(mapper.Map<RegisterAccountModel>(request));
            return Ok(mapper.Map<PresintationAccountModel>(user));
        }
        catch (ProcessException e)
        {
            return BadRequest(e.Message);
        }
    }
}
