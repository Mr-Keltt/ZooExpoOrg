namespace ZooExpoOrg.Api.Controllers.Clients;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Animals;
using AutoMapper;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Context.Entities;

public class PresintationCreateClientModel
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }
}

public class PresintationCreateClientModelProfile : Profile
{
    public PresintationCreateClientModelProfile()
    {
        CreateMap<PresintationCreateClientModel, CreateClientModel>();
    }
}