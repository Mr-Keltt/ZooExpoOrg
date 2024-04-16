namespace ZooExpoOrg.Services.Accounts;

public interface IAccountService
{
    Task<bool> IsEmpty();


    Task<IEnumerable<AccountModel>> GetAll();
    Task<AccountModel> Create(RegisterAccountModel model);
}
