using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using ZooExpoOrg.Web.Services.Accounts;
using ZooExpoOrg.Web.Services.Clients;

namespace ZooExpoOrg.Web.Services.GetIdHelper;

public class GetIdHelperService : IGetIdHelperService
{
    private readonly ILocalStorageService localStorage;
    private readonly IAccountService accountService;
    private readonly IClientService clientService;
    private const string NavigationMenuVisibleKey = "authToken";

    public GetIdHelperService(
        ILocalStorageService localStorage,
        IAccountService accountService,
        IClientService clientService
        )
    {
        this.localStorage = localStorage;
        this.accountService = accountService;
        this.clientService = clientService;
    }

    public async Task<GetIdResult> GetAdminId()
    {
        var getIdResult = new GetIdResult();

        var getUsersResult = await accountService.GetUsers();

        if (!getUsersResult.Successful)
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = getUsersResult.ErrorMesage;

            return getIdResult;
        }

        var users = getUsersResult.Users;

        if (!users.Any())
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = "Users not found.";

            return getIdResult;
        }

        var admin = users.FirstOrDefault(x => x.UserName == Settings.AdminUserName);

        if (admin == null)
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = "Admin not found.";

            return getIdResult;
        }

        getIdResult.Successful = true;
        getIdResult.Id = admin.Id;

        return getIdResult;
    }

    public async Task<GetIdResult> GetUserId(CancellationToken cancellationToken = default)
    {
        string jwtToken = await localStorage.GetItemAsync<string>(NavigationMenuVisibleKey, cancellationToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

        var userIdString = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

        GetIdResult getIdResult = new GetIdResult();

        if (Guid.TryParse(userIdString, out Guid userId))
        {
            getIdResult.Successful = true;
            getIdResult.Id = userId;
        }
        else
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = "Incorrect Guid format.";
        }

        return getIdResult;
    }

    public async Task<GetIdResult> GetClientId()
    {
        var getIdResult = new GetIdResult();

        var getClientsResult = await clientService.GetClients();

        if (!getClientsResult.Successful)
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = "Clints not found.";

            return getIdResult;
        }

        var getUserId = await GetUserId();

        if (!getUserId.Successful)
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = getUserId.ErrorMesage;

            return getIdResult;
        }

        var client = getClientsResult.Clients.FirstOrDefault(x => x.UserId == getUserId.Id);

        if (client == null)
        {
            getIdResult.Successful = false;
            getIdResult.ErrorMesage = "Client not found.";

            return getIdResult;
        }

        getIdResult.Successful = true;
        getIdResult.Id = client.Id;

        return getIdResult;
    }
}
