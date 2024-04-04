using AutoMapper;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;

namespace ZooExpoOrg.Services.Clients; 

public class UpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid PhotoId { get; set; }

    //public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    //public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalModel> Animals { get; set; }

    //public virtual ICollection<Comment> Comments { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<UpdateClientModel, ClientEntity>();
    }
}