﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ZooExpoOrg.Context.Settings;
using ZooExpoOrg.Services.Settings;

namespace ZooExpoOrg.Context;

public static class Bootstrapper
{
    /// <summary>
    /// Register db context
    /// </summary>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString, settings.Type, true);

        services.AddDbContextFactory<MainDbContext>(dbInitOptionsDelegate);

        return services;
    }
}
