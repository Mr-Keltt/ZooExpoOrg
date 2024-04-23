using AutoMapper;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Expositions;

namespace ZooExpoOrg.Api.Controllers.Expositions;

public class PresintationUpdateExpositionModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}

public class PresintationUpdateExpositionModelProfile : Profile
{
    public PresintationUpdateExpositionModelProfile()
    {
        CreateMap<PresintationUpdateExpositionModel, UpdateExpositionModel>();
    }
}
