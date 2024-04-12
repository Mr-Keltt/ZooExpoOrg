namespace ZooExpoOrg.Services.Photos;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddPhotoService(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoService, PhotoService>();
        services.AddAutoMapper(typeof(CreatePhotoModelProfile));
        services.AddAutoMapper(typeof(PhotoModelProfile));

        return services;
    }
}
