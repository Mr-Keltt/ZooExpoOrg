namespace ZooExpoOrg.Web.Services.Accounts;

public interface IAccountService
{
    Task<GetUsersResult> GetUsers();

    Task<RegisterResult> RegisterAccount(RegisterAccountModel model);
}