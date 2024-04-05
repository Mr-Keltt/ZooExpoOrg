using AutoMapper;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Clients;

namespace ZooExpoOrg.Api.Controllers.Clients; 

public class PresintationUpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid PhotoId { get; set; }

    public virtual ICollection<Guid> Subscriptions { get; set; }

    public virtual ICollection<Guid> OrganizedExpositions { get; set; }

    public virtual ICollection<Guid> Animals { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<PresintationUpdateClientModel, ClientModel>();
    }
}