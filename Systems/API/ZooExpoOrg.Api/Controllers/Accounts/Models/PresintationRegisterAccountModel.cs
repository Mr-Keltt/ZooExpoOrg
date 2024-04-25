using FluentValidation;
using ZooExpoOrg.Context.Entities;
using AutoMapper;

namespace ZooExpoOrg.Services.Accounts;

public class PresintationRegisterAccountModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class PresintationRegisterAccountModelProfile : Profile
{
    public PresintationRegisterAccountModelProfile()
    {
        CreateMap<PresintationRegisterAccountModel, RegisterAccountModel>();
    }
}