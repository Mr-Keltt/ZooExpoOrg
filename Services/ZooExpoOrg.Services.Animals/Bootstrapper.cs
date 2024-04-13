namespace ZooExpoOrg.Services.Animals;

using Microsoft.Extensions.DependencyInjection;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalService(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalService, AnimalService>();
        services.AddSingleton<IAchievementService, AchievementService>();
        services.AddAutoMapper(typeof(AnimalModelProfile));
        services.AddAutoMapper(typeof(CreateAnimalModelProfile));
        services.AddAutoMapper(typeof(AchievementModelProfile));
        services.AddAutoMapper(typeof(CreateAchievementModelProfile));

        return services;
    }
}
