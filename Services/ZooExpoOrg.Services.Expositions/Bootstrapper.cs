using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Expositions;

public static class Bootstrapper
{
    public static IServiceCollection AddExpositionService(this IServiceCollection services)
    {
        services.AddSingleton<IExpositionService, ExpositionService>();

        return services;
    }
}
