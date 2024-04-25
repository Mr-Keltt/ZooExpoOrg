using ZooExpoOrg.Web.Services.GetRsultHelper.Models;

namespace ZooExpoOrg.Web.Services.Accounts;

public class AccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}