using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace ZooExpoOrg.Services.Expositions;

public static class Bootstrapper
{
    public static IServiceCollection AddExpositionService(this IServiceCollection services)
    {
        services.AddSingleton<IExpositionService, ExpositionService>();
        services.AddAutoMapper(typeof(ExpositionModelProfile));
        services.AddAutoMapper(typeof(CreateExpositionModelProfile));

        return services;
    }
}
