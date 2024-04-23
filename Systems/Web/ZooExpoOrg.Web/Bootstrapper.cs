using MudBlazor.Services;

namespace ZooExpoOrg.Web;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMudServices()
            ;

        return service;
    }
}
