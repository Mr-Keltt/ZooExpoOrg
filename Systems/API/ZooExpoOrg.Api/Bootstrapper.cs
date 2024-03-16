using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.RabbitMq;
using ZooExpoOrg.Services.Settings;

namespace ZooExpoOrg.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddRabbitMq();

        return service;
    }
}
