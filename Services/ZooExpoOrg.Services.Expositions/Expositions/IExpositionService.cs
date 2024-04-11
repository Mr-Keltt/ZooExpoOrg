namespace ZooExpoOrg.Services.Expositions;

public interface IExpositionService
{
    Task<IEnumerable<ExpositionModel>> GetAll();
    Task<ExpositionModel> GetById(Guid id);
    Task<ExpositionModel> Create(CreateExpositionModel model);
    Task Update(Guid id, UpdateExpositionModel model);
    Task<IEnumerable<Guid>> GetAllSubscribers();
    Task Subscribe(Guid userId);
    Task Unsubscribe(Guid userId);
    Task<IEnumerable<Guid>> GetAllParticipants();
    Task AddParticipant(Guid animalId);
    Task DeleteParticipant(Guid animalId);
    Task Delete(Guid id);
}
