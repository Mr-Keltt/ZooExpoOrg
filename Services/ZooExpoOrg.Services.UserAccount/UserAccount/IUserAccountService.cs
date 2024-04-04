namespace ZooExpoOrg.Services.UserAccount;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    Task<AccountModel> Create(RegisterAccountModel model);
}
