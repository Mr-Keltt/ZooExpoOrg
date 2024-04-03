namespace ZooExpoOrg.Api.Controllers.Clients;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Animals;
using AutoMapper;
using ZooExpoOrg.Services.Clients;

public class PresintationClientModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid PhotoId { get; set; }

    //public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    //public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalModel> Animals { get; set; }

    //public virtual ICollection<Comment> Comments { get; set; }
}

public class PresintationClientModelProfile : Profile
{
    public PresintationClientModelProfile()
    {
        CreateMap<PresintationClientModel, ClientModel>()
            .ReverseMap();
    }
}