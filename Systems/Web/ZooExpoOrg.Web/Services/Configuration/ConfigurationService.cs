using Blazored.LocalStorage;

namespace ZooExpoOrg.Web.Services.Configuration;

public class ConfigurationService : IConfigurationService
{
    private const string NavigationMenuVisibleKey = "navigationMenuVisible";
    private readonly ILocalStorageService localStorage;

    public ConfigurationService(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public async Task<bool> GetNavigationMenuVisibleAsync(CancellationToken cancellationToken = default)
    {
        return await localStorage.GetItemAsync<bool>(NavigationMenuVisibleKey, cancellationToken);
    }

    public async Task SetNavigationMenuVisibleAsync(bool value, CancellationToken cancellationToken = default)
    {
        await localStorage.SetItemAsync(NavigationMenuVisibleKey, value, cancellationToken);
    }
}