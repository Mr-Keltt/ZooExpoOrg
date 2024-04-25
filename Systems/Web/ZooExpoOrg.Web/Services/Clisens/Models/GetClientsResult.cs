using ZooExpoOrg.Web.Shared.Interfaces;

namespace ZooExpoOrg.Web.Services.Clients;

public class GetClientsResult : RequestResult
{
    public IEnumerable<VueClientModel> Clients { get; set; }
}
