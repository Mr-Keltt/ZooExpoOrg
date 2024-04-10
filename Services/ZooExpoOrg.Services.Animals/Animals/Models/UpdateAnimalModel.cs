using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;
using ZooExpoOrg.Services.Photos;
using static ZooExpoOrg.Services.Animals.CreateAnimalModelProfile;

namespace ZooExpoOrg.Services.Animals;

public class UpdateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }
}

public class UpdateAnimalModelProfile : Profile
{
    public UpdateAnimalModelProfile()
    {
        CreateMap<UpdateAnimalModel, AnimalEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight));
    }
}