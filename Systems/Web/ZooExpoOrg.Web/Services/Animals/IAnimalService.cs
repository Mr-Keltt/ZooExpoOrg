namespace ZooExpoOrg.Web.Services.Animals;

public interface IAnimalService
{
    Task<IEnumerable<VueAnimalModel>> GetAnimalsOwned(Guid ownerId);
    Task<VueAnimalModel> GetAnimal(Guid animalId);
    Task AddAnimal(VueCreateAnimalModel model);
    Task UpdateAnimal(Guid animalId, VueUpdateAnimalModel model);
    Task DeleteAnimal(Guid animalId);
}
