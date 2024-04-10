using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Clients;


public static class Bootstrapper
{
    public static IServiceCollection AddClienService(this IServiceCollection services)
    {
        services.AddSingleton<IClientService, ClientService>();
        services.AddAutoMapper(typeof(CreateClientModelProfile));

        return services;
    }
}
