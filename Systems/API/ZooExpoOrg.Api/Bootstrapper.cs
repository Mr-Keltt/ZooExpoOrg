using ZooExpoOrg.Api.Settings;
using ZooExpoOrg.Context.Seeder;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Photos;
using ZooExpoOrg.Services.RabbitMq;
using ZooExpoOrg.Services.Settings;
using ZooExpoOrg.Services.Accounts;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Expositions;
using ZooExpoOrg.Services.Comments;

namespace ZooExpoOrg.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration = null)
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
            .AddClienService()
            .AddExpositionService()
            .AddCommentService()
            .AddDbSeeder()
            ;

        return service;
    }
}
