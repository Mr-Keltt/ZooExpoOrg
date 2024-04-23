namespace ZooExpoOrg.Web.Services.Accounts;

public interface IAccountService
{
    Task RegisterAccount(RegisterAccountModel model);
}
