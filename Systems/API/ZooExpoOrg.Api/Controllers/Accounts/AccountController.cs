namespace ZooExpoOrg.Api.Controllers.Accounts;

using AutoMapper;
using ZooExpoOrg.Services.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Account")]
[Route("v{version:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountController> logger;
    private readonly IUserAccountService userAccountService;

    public AccountController(IMapper mapper, ILogger<AccountController> logger, IUserAccountService userAccountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
    }

    [HttpPost("")]
    public async Task<PresintationAccountModel> Register([FromQuery] RegisterAccountModel request)
    {
        var user = await userAccountService.Create(request);
        return mapper.Map<PresintationAccountModel>(user);
    }
}
