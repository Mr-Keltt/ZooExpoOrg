using ZooExpoOrg.Api.Settings;
using ZooExpoOrg.Services.Animals;
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
            .AddRabbitMq()
            .AddApiSpecialSettings()
            .AddAnimalService();

        return service;
    }
}
