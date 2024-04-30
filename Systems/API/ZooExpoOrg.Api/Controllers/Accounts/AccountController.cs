namespace ZooExpoOrg.Api.Controllers.Accounts;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using ZooExpoOrg.Services.Accounts;
using ZooExpoOrg.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using ZooExpoOrg.Common.Security;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ZooExpoOrg.Context.Entities;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Account")]
[Route("v{version:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountController> logger;
    private readonly IAccountService accountService;
	private readonly UserManager<UserEntity> userManager;

	public AccountController(
        IMapper mapper, 
        ILogger<AccountController> logger, 
        IAccountService accountService,
		IHttpContextAccessor httpContextAccessor,
		UserManager<UserEntity> userManager
		)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.accountService = accountService;
		this.userManager = userManager;
	}

	[HttpGet("all")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> GetAll()
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
			var user = await accountService.Create(mapper.Map<RegisterAccountModel>(request), HttpContext, Url);

			return Ok(mapper.Map<PresintationAccountModel>(user));
        }
        catch (ProcessException e)
        {
            return BadRequest(e.Message);
        }
        catch (ValidationException e) 
        {
            return BadRequest(e.Errors);
        }
    }

	[HttpGet("")]
	[AllowAnonymous]
	public async Task ConfirmEmail(string userId, string code)
	{
		if (userId == null)
		{
			throw new ProcessException("UserId is required");
		}

		if (code == null)
		{
			throw new ProcessException("Code is required");
        }

		var user = await userManager.FindByIdAsync(userId);

		if (user == null)
		{
			throw new ProcessException($"User (ID={userId}) not found");
        }

		var result = await userManager.ConfirmEmailAsync(user, code);
	}
}
