using ZooExpoOrg.Web.Shared.Interfaces;

namespace ZooExpoOrg.Web.Services.Accounts;

public class GetUsersResult : RequestResult
{
    public IEnumerable<AccountModel> Users { get; set; }
}
