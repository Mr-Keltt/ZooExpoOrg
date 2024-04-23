using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Photos;

namespace ZooExpoOrg.Web.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task RegisterAccount(RegisterAccountModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/account", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}

