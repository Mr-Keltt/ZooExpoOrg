namespace ZooExpoOrg.Services.Achievements;

using Microsoft.Extensions.DependencyInjection;

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

