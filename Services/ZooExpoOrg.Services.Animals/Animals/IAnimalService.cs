namespace ZooExpoOrg.Services.Animals.Animals;

public interface IAnimalService
{
    Task<IEnumerable<AnimalModel>> GetOwned(Guid ownerId);
    Task<AnimalModel> GetById(Guid id);
    Task<AnimalModel> Create(CreateAnimalModel model);
    Task Update(Guid id, UpdateAnimalModel model);
    Task Delete(Guid id);
}
