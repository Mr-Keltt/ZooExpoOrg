using ZooExpoOrg.Web.Shared.Interfaces;

namespace ZooExpoOrg.Web.Services.Clients;

public class GetClientResult : RequestResult
{
    public VueClientModel Client { get; set; }
}
