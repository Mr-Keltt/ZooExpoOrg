using ZooExpoOrg.Services.Settings;

namespace ZooExpoOrg.Identity;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            ;

        return services;
    }
}
