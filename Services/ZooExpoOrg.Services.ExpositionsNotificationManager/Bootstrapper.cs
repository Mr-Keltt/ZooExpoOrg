using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Services.ExpositionsNotificationManager.ExpositionsNotificationManager;

namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public static class Bootstrapper
{
    public static IServiceCollection AddExpositionsNotificationManagerService(this IServiceCollection services)
    {
        services.AddSingleton<IExpositionsNotificationManagerService, ExpositionsNotificationManagerService>();

        return services;
    }
}
