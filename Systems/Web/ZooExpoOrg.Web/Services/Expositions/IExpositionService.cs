namespace ZooExpoOrg.Web.Services.Expositions;

public interface IExpositionService
{
    Task<IEnumerable<VueExpositionModel>> GetExpositions();
    Task<VueExpositionModel> GetExposition(Guid expositionId);
    Task AddExposition(VueCreateExpositionModel model);
    Task EditExposition(Guid expositionId, VueUpdateExpositionModel model);
    Task Subscribe(Guid expositionId, Guid clientId);
    Task Unsubscribe(Guid expositionId, Guid clientId);
    Task AddParticipant(Guid expositionId, Guid animalId);
    Task DeleteParticipant(Guid expositionId, Guid animalId);
    Task DeleteExposition(Guid expositionId);
}
