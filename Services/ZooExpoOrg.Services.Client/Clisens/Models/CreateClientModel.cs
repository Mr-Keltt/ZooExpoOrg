using AutoMapper;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Clients;

public class CreateClientModel
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }
}

public class CreateClientModelProfile : Profile
{
    public CreateClientModelProfile()
    {
        CreateMap<CreateClientModel, ClientEntity>();
    }
}