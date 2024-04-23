namespace ZooExpoOrg.Services.WebConfiguration;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddWebConfigurationService(this IServiceCollection services)
    {
        services.AddSingleton<IWebConfigurationService, WebConfigurationService>();

        return services;
    }
}
