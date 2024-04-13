using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Comments;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalService(this IServiceCollection services)
    {
        services.AddSingleton<ICommentService, CommentService>();

        return services;
    }
}