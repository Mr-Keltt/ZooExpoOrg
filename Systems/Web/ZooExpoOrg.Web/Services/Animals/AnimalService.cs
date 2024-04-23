using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Expositions;

namespace ZooExpoOrg.Web.Services.Animals;

public class AnimalService : IAnimalService
{
    private readonly HttpClient httpClient;

    public AnimalService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VueAnimalModel>> GetAnimalsOwned(Guid ownerId)
    {
        var response = await httpClient.GetAsync($"v1/owned/{ownerId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VueAnimalModel>>() ?? new List<VueAnimalModel>();
    }

    public async Task<VueAnimalModel> GetAnimal(Guid animalId)
    {
        var response = await httpClient.GetAsync($"v1/animal/{animalId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VueAnimalModel>() ?? new();
    }

    public async Task AddAnimal(VueCreateAnimalModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/animal", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task UpdateAnimal(Guid animalId, VueUpdateAnimalModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/animal/{animalId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteAnimal(Guid animalId)
    {
        var response = await httpClient.DeleteAsync($"v1/animal/{animalId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
