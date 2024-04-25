using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Expositions;

public interface IExpositionService
{
    Task<GetModelResult<List<VueExpositionModel>>> GetExpositions();
    Task<GetModelResult<VueExpositionModel>> GetExposition(Guid expositionId);
    Task<ManageModelResult<VueExpositionModel>> AddExposition(VueCreateExpositionModel model);
    Task<ManageModelResult<VueExpositionModel>> EditExposition(Guid expositionId, VueUpdateExpositionModel model);
    Task Subscribe(Guid expositionId, Guid clientId);
    Task Unsubscribe(Guid expositionId, Guid clientId);
    Task AddParticipant(Guid expositionId, Guid animalId);
    Task DeleteParticipant(Guid expositionId, Guid animalId);
    Task<DeleteModelResult> DeleteExposition(Guid expositionId);
}
