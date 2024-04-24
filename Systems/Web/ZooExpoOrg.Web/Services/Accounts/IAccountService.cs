namespace ZooExpoOrg.Web.Services.Accounts;

public interface IAccountService
{
    Task<RegisterResult> RegisterAccount(RegisterAccountModel model);
}
