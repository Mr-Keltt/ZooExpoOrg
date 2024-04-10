using AutoMapper;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Clients; 

public class UpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid? PhotoId { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<UpdateClientModel, ClientEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.PhotoId));
    }
}