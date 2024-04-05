﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Accounts;

public static class Bootstrapper
{
    public static IServiceCollection AddAccountService(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
