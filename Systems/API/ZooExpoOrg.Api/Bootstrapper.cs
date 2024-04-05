using ZooExpoOrg.Api.Settings;
using ZooExpoOrg.Context.Seeder;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Photos;
using ZooExpoOrg.Services.RabbitMq;
using ZooExpoOrg.Services.Settings;
using ZooExpoOrg.Services.Accounts;

namespace ZooExpoOrg.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddRabbitMq()
            .AddApiSpecialSettings()
            .AddAnimalService()
            .AddPhotoService()
            .AddAccountService()
            .AddDbSeeder();

        return service;
    }
}
