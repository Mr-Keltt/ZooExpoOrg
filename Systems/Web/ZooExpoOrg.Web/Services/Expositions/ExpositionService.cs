using System.Net;
using System.Net.Http.Json;
using System.Reflection;

namespace ZooExpoOrg.Web.Services.Expositions;

public class ExpositionService : IExpositionService
{
    private readonly HttpClient httpClient;

    public ExpositionService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VueExpositionModel>> GetExpositions()
    {
        var response = await httpClient.GetAsync("v1/exposition");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VueExpositionModel>>() ?? new List<VueExpositionModel>();
    }

    public async Task<VueExpositionModel> GetExposition(Guid expositionId)
    {
        var response = await httpClient.GetAsync($"v1/exposition/{expositionId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VueExpositionModel>() ?? new();
    }

    public async Task AddExposition(VueCreateExpositionModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/exposition", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditExposition(Guid expositionId, VueUpdateExpositionModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/exposition/{expositionId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
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

    public async Task DeleteExposition(Guid expositionId)
    {
        var response = await httpClient.DeleteAsync($"v1/exposition/{expositionId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
