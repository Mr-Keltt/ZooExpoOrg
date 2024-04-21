namespace ZooExpoOrg.Api.Controllers.Clients;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Animals;
using AutoMapper;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Context.Entities;

public class PresintationClientModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid PhotoId { get; set; }

    public virtual ICollection<Guid> Subscriptions { get; set; }

    public virtual ICollection<Guid> OrganizedExpositions { get; set; }

    public virtual ICollection<Guid> Animals { get; set; }

    public virtual ICollection<Guid> Comments { get; set; }

    public virtual ICollection<Guid> UnreadNotifications { get; set; }
}

public class PresintationClientModelProfile : Profile
{
    public PresintationClientModelProfile()
    {
        CreateMap<ClientModel, PresintationClientModel>();
    }
}