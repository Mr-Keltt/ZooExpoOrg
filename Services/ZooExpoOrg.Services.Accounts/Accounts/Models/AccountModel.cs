using AutoMapper;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Accounts;

public class AccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class AccountModelProfile : Profile  
{
    public AccountModelProfile()
    {
        CreateMap<UserEntity, AccountModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}