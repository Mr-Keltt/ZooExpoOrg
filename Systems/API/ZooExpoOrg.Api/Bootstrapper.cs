using System.Runtime.CompilerServices;
using ZooExpoOrg.Settings;

namespace ZooExpoOrg.Api;

public class Bootstrapper
{
    public static IServiceCollection RegisterServices(IServiceCollection service)
    {
        service
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings();

        return service;
    }
}
