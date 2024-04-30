using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Clients;

public interface IClientService
{
    Task<GetModelResult<List<VueClientModel>>> GetClients();
    Task<GetModelResult<VueClientModel>> GetClient(Guid clientId);
    Task<ManageModelResult<VueClientModel>> AddClient(VueCreateClientModel model);
    Task<ManageModelResult<VueClientModel>> UpdateClients(Guid clientId, VueUpdateClientModel model);
    Task<DeleteModelResult> DeleteClients(Guid clientId);
}
