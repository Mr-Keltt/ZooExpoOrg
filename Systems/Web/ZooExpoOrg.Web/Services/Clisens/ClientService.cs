using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Accounts;
using ZooExpoOrg.Web.Services.Animals;

namespace ZooExpoOrg.Web.Services.Clients;

public class ClientService : IClientService
{
    private readonly HttpClient httpClient;

    public ClientService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetClientsResult> GetClients()
    {
        var getClientsResult = new GetClientsResult();

        var response = await httpClient.GetAsync($"v1/client");

        if (!response.IsSuccessStatusCode)
        {
            getClientsResult.Successful = false;
            getClientsResult.ErrorMesage = "Clients not found.";

            return getClientsResult;
        }

        getClientsResult.Successful = true;
        getClientsResult.Clients = await response.Content.ReadFromJsonAsync<IEnumerable<VueClientModel>>() ?? new List<VueClientModel>();

        return getClientsResult;
    }

    public async Task<GetClientResult> GetClient(Guid clientId)
    {
        var getClientResult = new GetClientResult();

        var response = await httpClient.GetAsync($"v1/client/{clientId}");

        if (!response.IsSuccessStatusCode)
        {
            getClientResult.Successful = false;
            getClientResult.ErrorMesage = "Client not found.";

            return getClientResult;
        }

        getClientResult.Successful = true;
        getClientResult.Client = await response.Content.ReadFromJsonAsync<VueClientModel>() ?? new VueClientModel();

        return getClientResult;
    }

    public async Task AddClients(VueCreateClientModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/client", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task UpdateClients(Guid clientId, VueUpdateClientModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/client/{clientId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteClients(Guid clientId)
    {
        var response = await httpClient.DeleteAsync($"v1/client/{clientId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
