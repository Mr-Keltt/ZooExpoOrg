using AutoMapper;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Animals.Animals;

namespace ZooExpoOrg.Api.Controllers.Animals;

public class PresintationCreateAnimalModel
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AnimalType Type { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }
}


public class PresintationCreateAnimalModelProfile : Profile
{
    public PresintationCreateAnimalModelProfile()
    {
        CreateMap<PresintationCreateAnimalModel, CreateAnimalModel>();
    }
}
