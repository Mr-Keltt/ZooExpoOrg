namespace ZooExpoOrg.Services.Photos;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddPhotoService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPhotoService, PhotoService>();
    }
}
