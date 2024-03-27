using AutoMapper;
using ZooExpoOrg.Api.Controllers.Photos;
using ZooExpoOrg.Services.Animals;

namespace ZooExpoOrg.Api.Controllers.Animals.Models;

public class PresintationUpdateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public ICollection<PresintationPhotoModel> Photos { get; set; }
}

public class PresintationUpdateAnimalModelProfile : Profile
{
    public PresintationUpdateAnimalModelProfile()
    {
        CreateMap<PresintationUpdateAnimalModel, UpdateAnimalModel>()
            .ReverseMap();
    }
}