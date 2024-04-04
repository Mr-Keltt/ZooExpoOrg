using AutoMapper;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Clients;

namespace ZooExpoOrg.Api.Controllers.Clients;

public class PresintationCreateClientModel
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

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
        CreateMap<PresintationCreateClientModel, ClientModel>()
            .ReverseMap();
    }
}