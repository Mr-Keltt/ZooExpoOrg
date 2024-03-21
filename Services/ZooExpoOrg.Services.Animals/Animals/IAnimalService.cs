namespace ZooExpoOrg.Services.Animals;

public interface IAnimalService
{
    Task<IEnumerable<AnimalModel>> GetAll();
    Task<AnimalModel> GetById(Guid id);
    Task<AnimalModel> Create(CreateAnimalModel model);
    Task Update(Guid id, UpdateAnimalModel model);
    Task Delete(Guid id);
}
