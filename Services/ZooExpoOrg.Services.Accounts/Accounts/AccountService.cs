using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<UserEntity> userManager;
    private readonly IModelValidator<RegisterAccountModel> registerUserAccountModelValidator;

    public AccountService(
        IMapper mapper,
        UserManager<UserEntity> userManager,
        IModelValidator<RegisterAccountModel> registerUserAccountModelValidator
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<AccountModel> Create(RegisterAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            UserName = model.UserName,
            Email = model.Email,
            EmailConfirmed = true, // TODO Сделать подтверждение почты 
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        //return mapper.Map<AccountModel>(user);
        return new AccountModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}

