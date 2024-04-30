using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;
using ZooExpoOrg.Context.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;

namespace ZooExpoOrg.Services.Clients;

public class ClientModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public virtual ICollection<Guid> Photos { get; set; }

    public virtual ICollection<Guid> OwnedPhotos { get; set; }

    public virtual ICollection<Guid> Subscriptions { get; set; }

    public virtual ICollection<Guid> OrganizedExpositions { get; set; }

    public virtual ICollection<Guid> Animals { get; set; }

    public virtual ICollection<Guid> Comments { get; set; }

    public virtual ICollection<Guid> UnreadNotifications { get; set; }
}

public class ClientModelProfile : Profile
{
    public ClientModelProfile()
    {
        CreateMap<ClientEntity, ClientModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions.Select(e => e.Uid)))
            .ForMember(dest => dest.OrganizedExpositions, opt => opt.MapFrom(src => src.OrganizedExpositions.Select(e => e.Uid)))
            .ForMember(dest => dest.Animals, opt => opt.MapFrom(src => src.Animals.Select(e => e.Uid)))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(e => e.Uid)))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(e => e.Uid)))
            .ForMember(dest => dest.OwnedPhotos, opt => opt.MapFrom(src => src.OwnedPhotos.Select(e => e.Uid)))
            .ForMember(dest => dest.UnreadNotifications, opt => opt.MapFrom(src => src.UnreadNotifications.Select(e => e.Uid)));
    }
}