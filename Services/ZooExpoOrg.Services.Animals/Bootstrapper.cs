namespace ZooExpoOrg.Services.Animals;

using Microsoft.Extensions.DependencyInjection;
using ZooExpoOrg.Services.Animals;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalService(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalService, AnimalService>();
        services.AddAutoMapper(typeof(AnimalModelProfile));
        services.AddAutoMapper(typeof(CreateAnimalModelProfile));

        return services;
    }
}
