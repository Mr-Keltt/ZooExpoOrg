using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Animals;

namespace ZooExpoOrg.Web.Services.Clients;

public class ClientService : IClientService
{
    private readonly HttpClient httpClient;

    public ClientService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VueClientModel>> GetClients()
    {
        var response = await httpClient.GetAsync($"v1/client");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VueClientModel>>() ?? new List<VueClientModel>();
    }

    public async Task<VueClientModel> GetClient(Guid clientId)
    {
        var response = await httpClient.GetAsync($"v1/client/{clientId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VueClientModel>() ?? new();
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
