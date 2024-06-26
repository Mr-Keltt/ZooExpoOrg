﻿using AutoMapper;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Expositions;

namespace ZooExpoOrg.Api.Controllers.Expositions;

public class PresintationExpositionModel
{
    public Guid Id { get; set; }

    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public AnimalType ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<Guid> Participants { get; set; }

    public virtual ICollection<Guid> Photos { get; set; }

    public virtual ICollection<Guid> Comments { get; set; }

    public virtual ICollection<Guid> Subscribers { get; set; }

    public virtual ICollection<Guid> SentNotifications { get; set; }
}

public class PresintationExpositionModelProfile : Profile
{
    public PresintationExpositionModelProfile()
    {
        CreateMap<ExpositionModel, PresintationExpositionModel>();
    }
}
