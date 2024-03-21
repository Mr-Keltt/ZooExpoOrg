namespace ZooExpoOrg.Services.Animals;

using Microsoft.Extensions.DependencyInjection;
using ZooExpoOrg.Services.Animals;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAnimalService, AnimalService>();
    }
}
