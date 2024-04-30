using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public static class Bootstrapper
{
    public static IServiceCollection AddExpositionsNotificationManagerService(this IServiceCollection services)
    {
        services.AddSingleton<IExpositionsNotificationManagerService, ExpositionsNotificationManagerService>();
        services.AddAutoMapper(typeof(CreateNotificationModelProfile));
        services.AddAutoMapper(typeof(NotificationModelProfile));

        return services;
    }
}
