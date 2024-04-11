namespace ZooExpoOrg.Services.Expositions;

public interface IExpositionService
{
    Task<IEnumerable<ExpositionModel>> GetAll();
    Task<ExpositionModel> GetById(Guid id);
    Task<ExpositionModel> Create(CreateExpositionModel model);
    Task Update(Guid id, UpdateExpositionModel model);
    Task<IEnumerable<Guid>> GetAllSubscribers(Guid id);
    Task Subscribe(Guid id, Guid userId);
    Task Unsubscribe(Guid id, Guid userId);
    Task<IEnumerable<Guid>> GetAllParticipants(Guid id);
    Task AddParticipant(Guid id, Guid animalId);
    Task DeleteParticipant(Guid id, Guid animalId);
    Task Delete(Guid id);
}
