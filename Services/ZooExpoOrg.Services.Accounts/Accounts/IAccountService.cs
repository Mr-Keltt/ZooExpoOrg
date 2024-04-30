using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZooExpoOrg.Services.Accounts;

public interface IAccountService
{
    Task<bool> IsEmpty();


    Task<IEnumerable<AccountModel>> GetAll();
    Task<AccountModel> Create(RegisterAccountModel model, HttpContext httpContext = default, IUrlHelper urlHelper = default);
}
