using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.UserAccount;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();
        //services.AddTransient<IValidator<RegisterAccountModel>, RegisterUserAccountModelValidator>();

        return services;
    }
}
