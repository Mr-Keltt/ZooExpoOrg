using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Api.Controllers.Animals.Models;

public class PresintationAnimalModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public Guid OwnerId { get; set; }

    //public virtual IEnumerable<CommentModel> Comments { get; set; }

    //public virtual IEnumerable<PhotoModel> Photos { get; set; }

    //public virtual IEnumerable<AchievementModel> Achievements { get; set; }
}


public class PresintationAnimalModelProfile : Profile
{
    public PresintationAnimalModelProfile()
    {
        CreateMap<PresintationAnimalModel, AnimalEntity>();
    }
}
