namespace ZooExpoOrg.Services.Accounts;

public interface IAccountService
{
    Task<bool> IsEmpty();

    Task<AccountModel> Create(RegisterAccountModel model);
}
