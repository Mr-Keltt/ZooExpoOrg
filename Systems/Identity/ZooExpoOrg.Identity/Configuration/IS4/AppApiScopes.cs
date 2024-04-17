namespace ZooExpoOrg.Identity.Configuration;
using Duende.IdentityServer.Models;
using ZooExpoOrg.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.UseScope, "UseScope")
        };
}