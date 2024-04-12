namespace ZooExpoOrg.Services.Expositions;

public interface IExpositionService
{
    Task<IEnumerable<ExpositionModel>> GetAll();
    Task<ExpositionModel> GetById(Guid id);
    Task<ExpositionModel> Create(CreateExpositionModel model);
    Task Update(Guid id, UpdateExpositionModel model);
    Task Subscribe(Guid id, Guid clientId);
    Task Unsubscribe(Guid id, Guid clientId);
    Task AddParticipant(Guid id, Guid animalId);
    Task DeleteParticipant(Guid id, Guid animalId);
    Task Delete(Guid id);
}
