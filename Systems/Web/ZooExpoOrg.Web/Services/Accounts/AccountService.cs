using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Auth;
using ZooExpoOrg.Web.Services.Photos;

namespace ZooExpoOrg.Web.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<RegisterResult> RegisterAccount(RegisterAccountModel model)
    {
        var requestContent = JsonContent.Create(model);

        var response = await httpClient.PostAsync("v1/account", requestContent);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);

        var registerResult = JsonSerializer.Deserialize<RegisterResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new RegisterResult();

        registerResult.Successful = response.IsSuccessStatusCode;

        return registerResult;
    }
}

