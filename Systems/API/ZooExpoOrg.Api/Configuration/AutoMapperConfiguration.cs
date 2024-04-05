namespace ZooExpoOrg.Api.Configuration;

using ZooExpoOrg.Common.Helpers;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        AutoMappersRegisterHelper.Register(services);

        return services;
    }
}