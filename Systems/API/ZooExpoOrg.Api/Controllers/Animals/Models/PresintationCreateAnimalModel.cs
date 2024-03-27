using AutoMapper;
using ZooExpoOrg.Api.Controllers.Animals;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;

namespace ZooExpoOrg.Api.Controllers.Animals
{
    public class PresintationCreateAnimalModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Breed { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public Guid OwnerId { get; set; }
    }
}

public class PresintationCreateAnimalModelProfile : Profile
{
    public PresintationCreateAnimalModelProfile()
    {
        CreateMap<PresintationCreateAnimalModel, CreateAnimalModel>()
            .ReverseMap();
    }
}
