namespace ZooExpoOrg.Api.Controllers.Clients;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Animals;
using AutoMapper;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Context.Entities;

public class PresintationUpdateClientModel
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }
}

public class PresintationUpdateClientModelProfile : Profile
{
    public PresintationUpdateClientModelProfile()
    {
        CreateMap<PresintationUpdateClientModel, UpdateClientModel>();
    }
}