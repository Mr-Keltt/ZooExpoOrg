using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Animals;

public interface IAnimalService
{
    Task<GetModelResult<List<VueAnimalModel>>> GetAnimalsOwned(Guid ownerId);
    Task<GetModelResult<VueAnimalModel>> GetAnimal(Guid animalId);
    Task<ManageModelResult<VueAnimalModel>> AddAnimal(VueCreateAnimalModel model);
    Task<ManageModelResult<VueAnimalModel>> UpdateAnimal(Guid animalId, VueUpdateAnimalModel model);
    Task<DeleteModelResult> DeleteAnimal(Guid animalId);
}
