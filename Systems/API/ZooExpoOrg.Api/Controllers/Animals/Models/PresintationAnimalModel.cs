using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Api.Controllers.Photos;

namespace ZooExpoOrg.Api.Controllers.Animals;

public class PresintationAnimalModel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public virtual IEnumerable<Guid> Comments { get; set; }

    public virtual IEnumerable<Guid> Photos { get; set; }

    public virtual IEnumerable<Guid> Achievements { get; set; }

    public virtual ICollection<Guid> Expositions { get; set; }
}


public class PresintationAnimalModelProfile : Profile
{
    public PresintationAnimalModelProfile()
    {
        CreateMap<AnimalModel, PresintationAnimalModel>()
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height != null ? src.Height : 0))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight != null ? src.Weight : 0));
    }
}
