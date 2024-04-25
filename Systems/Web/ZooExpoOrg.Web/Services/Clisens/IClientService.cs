namespace ZooExpoOrg.Web.Services.Clients;

public interface IClientService
{
    Task<GetClientsResult> GetClients();
    Task<GetClientResult> GetClient(Guid clientId);
    Task AddClients(VueCreateClientModel model);
    Task UpdateClients(Guid clientId, VueUpdateClientModel model);
    Task DeleteClients(Guid clientId);
}
