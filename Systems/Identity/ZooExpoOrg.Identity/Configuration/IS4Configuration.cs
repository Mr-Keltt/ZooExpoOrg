namespace ZooExpoOrg.Identity.Configuration;

using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using Microsoft.AspNetCore.Identity;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<ApiUser, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<ApiUser>>()
            .AddDefaultTokenProviders()
            ;

        services
            .AddIdentityServer()
            
            .AddAspNetIdentity<ApiUser>()

            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            //.AddTestUsers(AppApiTestUsers.ApiUsers)
            
            //.AddDeveloperSigningCredential()
            ;

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}

