namespace ZooExpoOrg.Web.Services.Configuration;

public interface IConfigurationService
{
    Task<bool> GetNavigationMenuVisibleAsync(CancellationToken cancellationToken = default);
    Task SetNavigationMenuVisibleAsync(bool value, CancellationToken cancellationToken = default);
}