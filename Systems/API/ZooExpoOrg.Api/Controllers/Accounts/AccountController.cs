namespace ZooExpoOrg.Api.Controllers.Accounts;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using ZooExpoOrg.Services.Accounts;

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

    [HttpPost("")]
    public async Task<PresintationAccountModel> Register([FromQuery] RegisterAccountModel request)
    {
        var user = await accountService.Create(request);
        return mapper.Map<PresintationAccountModel>(user);
    }
}
