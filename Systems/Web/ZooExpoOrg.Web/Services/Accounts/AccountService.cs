using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Auth;
using ZooExpoOrg.Web.Services.Clients;
using ZooExpoOrg.Web.Services.Photos;

namespace ZooExpoOrg.Web.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetUsersResult> GetUsers()
    {
        var getUsersResult = new GetUsersResult();

        var response = await httpClient.GetAsync("v1/account");

        if (!response.IsSuccessStatusCode)
        {
            getUsersResult.Successful = false;
            getUsersResult.ErrorMesage = "Users not found.";

            return getUsersResult;
        }

        getUsersResult.Successful = true;
        getUsersResult.Users = await response.Content.ReadFromJsonAsync<IEnumerable<AccountModel>>() ?? new List<AccountModel>();

        return getUsersResult;
    }

    public async Task<RegisterResult> RegisterAccount(RegisterAccountModel model)
    {
        var registerResult = new RegisterResult();

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/account", requestContent);

        if (response.IsSuccessStatusCode)
        {
            registerResult.Successful = true;

            return registerResult;
        }

        var content = await response.Content.ReadAsStringAsync();

        registerResult = JsonSerializer.Deserialize<RegisterResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new RegisterResult();
        registerResult.Successful = false;
        registerResult.ErrorMesage = "Register error.";

        return registerResult;
    }
}

