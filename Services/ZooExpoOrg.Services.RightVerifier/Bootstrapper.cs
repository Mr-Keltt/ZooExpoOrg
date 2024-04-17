using Microsoft.Extensions.DependencyInjection;

namespace ZooExpoOrg.Services.RightVerifier;

public static class Bootstrapper
{
    public static IServiceCollection AddRightVerifier(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRightVerifierService, RightVerifierService>();
    }
}
