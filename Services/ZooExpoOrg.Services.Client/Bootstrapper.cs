using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Clients;


public static class Bootstrapper
{
    public static IServiceCollection AddClienService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IClientService, ClientService>();
    }
}
