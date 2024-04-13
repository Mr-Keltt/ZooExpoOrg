using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Comments;

public static class Bootstrapper
{
    public static IServiceCollection AddCommentService(this IServiceCollection services)
    {
        services.AddSingleton<ICommentService, CommentService>();
        services.AddAutoMapper(typeof(CreateCommentModelProfile));
        services.AddAutoMapper(typeof(CommentModelProfile));

        return services;
    }
}