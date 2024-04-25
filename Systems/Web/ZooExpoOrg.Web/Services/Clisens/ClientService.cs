using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Accounts;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Clients;

public class ClientService : IClientService
{
    private readonly HttpClient httpClient;

    public ClientService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VueClientModel>>> GetClients()
    {
        var response = await httpClient.GetAsync($"v1/client");
        
        var getResultHelper = new GetResultHelper<List<VueClientModel>>();
        
        return await getResultHelper.GetGetModelResult(response, "Clients");
    }

    public async Task<GetModelResult<VueClientModel>> GetClient(Guid clientId)
    {
        var response = await httpClient.GetAsync($"v1/client/{clientId}");

        GetResultHelper<VueClientModel> getResultHelper = new GetResultHelper<VueClientModel>();

        return await getResultHelper.GetGetModelResult(response, "Client");
    }

    public async Task<ManageModelResult<VueClientModel>> AddClient(VueCreateClientModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/client", requestContent);

        var getResultHelper = new GetResultHelper<VueClientModel>();

        return await getResultHelper.GetManageModelResult(response, "Client add");
    }

    public async Task<ManageModelResult<VueClientModel>> UpdateClients(Guid clientId, VueUpdateClientModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/client/{clientId}", requestContent);

        var getResultHelper = new GetResultHelper<VueClientModel>();

        return await getResultHelper.GetManageModelResult(response, "Client update");
    }

    public async Task<DeleteModelResult> DeleteClients(Guid clientId)
    {
        var response = await httpClient.DeleteAsync($"v1/client/{clientId}");

        var content = await response.Content.ReadAsStringAsync();

        var getResultHelper = new GetResultHelper<VueClientModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Client");
    }
}
