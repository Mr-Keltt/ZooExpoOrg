using AutoMapper;
using ZooExpoOrg.Api.Controllers.Accounts;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.UserAccount;

namespace ZooExpoOrg.Api.Controllers.Accounts
{
    public class PresintationAccountModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

}

public class PresintationAccountModelProfile : Profile
{
    public PresintationAccountModelProfile()
    {
        CreateMap<AccountModel, PresintationAccountModel>();
    }
}