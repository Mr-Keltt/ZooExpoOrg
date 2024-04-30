using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Achievements;
using ZooExpoOrg.Web.Services.Expositions;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Animals;

public class AnimalService : IAnimalService
{
    private readonly HttpClient httpClient;

    public AnimalService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VueAnimalModel>>> GetAnimalsOwned(Guid ownerId)
    {
        var response = await httpClient.GetAsync($"v1/animal/owned/{ownerId}");

        var getResultHelper = new GetResultHelper<List<VueAnimalModel>>();

        return await getResultHelper.GetGetModelResult(response, "Animals");
    }

    public async Task<GetModelResult<VueAnimalModel>> GetAnimal(Guid animalId)
    {
        var response = await httpClient.GetAsync($"v1/animal/{animalId}");

        var getResultHelper = new GetResultHelper<VueAnimalModel>();

        return await getResultHelper.GetGetModelResult(response, "Animal");
    }

    public async Task<ManageModelResult<VueAnimalModel>> AddAnimal(VueCreateAnimalModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/animal", requestContent);

        var getResultHelper = new GetResultHelper<VueAnimalModel>();

        return await getResultHelper.GetManageModelResult(response, "Animal add");
    }

    public async Task<ManageModelResult<VueAnimalModel>> UpdateAnimal(Guid animalId, VueUpdateAnimalModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/animal/{animalId}", requestContent);

        var getResultHelper = new GetResultHelper<VueAnimalModel>();

        return await getResultHelper.GetManageModelResult(response, "Animal update");
    }

    public async Task<DeleteModelResult> DeleteAnimal(Guid animalId)
    {
        var response = await httpClient.DeleteAsync($"v1/animal/{animalId}");

        var getResultHelper = new GetResultHelper<VueAnimalModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Animal");
    }
}
