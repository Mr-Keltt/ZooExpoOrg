using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.Users;


public static class Bootstrapper
{
    public static IServiceCollection AddUserService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserService, UserService>();
    }
}
