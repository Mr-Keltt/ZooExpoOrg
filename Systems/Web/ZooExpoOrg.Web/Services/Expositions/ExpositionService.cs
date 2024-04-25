using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Expositions;

public class ExpositionService : IExpositionService
{
    private readonly HttpClient httpClient;

    public ExpositionService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VueExpositionModel>>> GetExpositions()
    {
        var response = await httpClient.GetAsync("v1/exposition");

        var getResultHelper = new GetResultHelper<List<VueExpositionModel>>();

        return await getResultHelper.GetGetModelResult(response, "Expositions");
    }

    public async Task<GetModelResult<VueExpositionModel>> GetExposition(Guid expositionId)
    {
        var response = await httpClient.GetAsync($"v1/exposition/{expositionId}");

        var getResultHelper = new GetResultHelper<VueExpositionModel>();

        return await getResultHelper.GetGetModelResult(response, "Exposition");
    }

    public async Task<ManageModelResult<VueExpositionModel>> AddExposition(VueCreateExpositionModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/exposition", requestContent);

        var getResultHelper = new GetResultHelper<VueExpositionModel>();

        return await getResultHelper.GetManageModelResult(response, "Exposition add");
    }

    public async Task<ManageModelResult<VueExpositionModel>> EditExposition(Guid expositionId, VueUpdateExpositionModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}", requestContent);

        var getResultHelper = new GetResultHelper<VueExpositionModel>();

        return await getResultHelper.GetManageModelResult(response, "Exposition update");
    }

    public async Task Subscribe(Guid expositionId, Guid clientId)
    {
        var requestContent = JsonContent.Create(new object());
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}/subscribe/{clientId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task Unsubscribe(Guid expositionId, Guid clientId)
    {
        var requestContent = JsonContent.Create(new object());
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}/unsubscribe/{clientId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task AddParticipant(Guid expositionId, Guid animalId)
    {
        var requestContent = JsonContent.Create(new object());
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}/participants/add/{animalId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteParticipant(Guid expositionId, Guid animalId)
    {
        var requestContent = JsonContent.Create(new object());
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}/participants/delete/{animalId:Guid}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<DeleteModelResult> DeleteExposition(Guid expositionId)
    {
        var response = await httpClient.DeleteAsync($"v1/exposition/{expositionId}");

        var getResultHelper = new GetResultHelper<VueExpositionModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Exposition");
    }
}
