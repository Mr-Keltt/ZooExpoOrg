using ZooExpoOrg.Web.Services.Auth;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Accounts;

public interface IAccountService
{
    Task<GetModelResult<List<AccountModel>>> GetUsers();

    Task<ManageModelResult<AccountModel>> RegisterAccount(RegisterAccountModel model);
}