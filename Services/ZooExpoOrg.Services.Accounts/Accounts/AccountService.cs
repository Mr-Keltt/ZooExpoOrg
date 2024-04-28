using AutoMapper;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace ZooExpoOrg.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<UserEntity> userManager;
    private readonly IModelValidator<RegisterAccountModel> registerUserAccountModelValidator;
	private readonly IEmailService emailService;

	public AccountService(
        IMapper mapper,
        UserManager<UserEntity> userManager,
        IModelValidator<RegisterAccountModel> registerUserAccountModelValidator,
		IEmailService emailService
	)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
		this.emailService = emailService;
	}

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<IEnumerable<AccountModel>> GetAll()
    {
        var result = userManager.Users;

        return mapper.Map<IEnumerable<AccountModel>>(result);
    }

    public async Task<AccountModel> Create(RegisterAccountModel model, HttpContext httpContext = default, IUrlHelper urlHelper = default)
    {
        registerUserAccountModelValidator.Check(model);

        var userByUserName = await userManager.FindByEmailAsync(model.UserName);

        if (userByUserName != null)
        {
            throw new ProcessException($"User account with ligin {model.UserName} already exist.");
        }

        var userByEmail = await userManager.FindByEmailAsync(model.Email);

        if (userByEmail != null)
        { 
            throw new ProcessException($"User account with email {model.Email} already exist.");
        }

        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            UserName = model.UserName,
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        }  

        if (httpContext != null && urlHelper != null)
        {
            await SendConfirmingEmail(model, user, httpContext, urlHelper);
		}    

        return mapper.Map<AccountModel>(user);
    }

	private async Task SendConfirmingEmail(RegisterAccountModel model, UserEntity user, HttpContext httpContext, IUrlHelper urlHelper)
    {
		var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

		var callbackUrl = urlHelper.Action(
		"ConfirmEmail",
        "Account",
        new { userId = user.Id, code = code },
		protocol: httpContext.Request.Scheme
        );

		await emailService.SendEmailAsync(model.Email, "Confirm your account",
			$"Подтвердите регистрацию на сайте ZooExpoOrg, перейдя по ссылке: <a href='{callbackUrl}'>Подтвердить</a>");
	}
}

