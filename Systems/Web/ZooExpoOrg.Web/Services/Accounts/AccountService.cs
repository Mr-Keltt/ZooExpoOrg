using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Auth;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<AccountModel>>> GetUsers()
    {
        var response = await httpClient.GetAsync("v1/account");

        var getResultHelper = new GetResultHelper<List<AccountModel>>();

        return await getResultHelper.GetGetModelResult(response, "Users");
    }

	public async Task<ManageModelResult<AccountModel>> RegisterAccount(RegisterAccountModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/account", requestContent);

        var getResultHelper = new GetResultHelper<AccountModel>();

        return await getResultHelper.GetManageModelResult(response, "Account register");
    }
}

