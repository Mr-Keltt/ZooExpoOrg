using AutoMapper;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.UserAccount;

public class AccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<UserEntity, AccountModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}