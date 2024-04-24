using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using ZooExpoOrg.Web.Services.Clients;

namespace ZooExpoOrg.Web.Services.GetIdHelper;

public class GetIdHelperService : IGetIdHelperService
{
    private readonly ILocalStorageService localStorage;
    private readonly IClientService clientService;
    private const string NavigationMenuVisibleKey = "authToken";

    public GetIdHelperService(
        ILocalStorageService localStorage,
        IClientService clientService
        )
    {
        this.localStorage = localStorage;
        this.clientService = clientService;
    }

    public async Task<Guid> GetUserId(CancellationToken cancellationToken = default)
    {
        string jwtToken = await localStorage.GetItemAsync<string>(NavigationMenuVisibleKey, cancellationToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

        var userIdString = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

        return Guid.Parse(userIdString);
    }

    public async Task<Guid> GetClientId(CancellationToken cancellationToken = default)
    {
        IEnumerable<VueClientModel> clients = await clientService.GetClients();

        Guid userId = await GetUserId();

        VueClientModel client = clients.FirstOrDefault(x => x.UserId == userId);

        return client.Id;
    }
}
