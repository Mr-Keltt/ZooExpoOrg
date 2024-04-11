using AutoMapper;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Expositions;

public class ExpositionModel
{
    public Guid Id { get; set; }

    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

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
}

public class ExpositionModelProfile : Profile
{
    public ExpositionModelProfile()
    {
        CreateMap<ExpositionEntity, ExpositionModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.Organizer.Uid))
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.Select(p => p.Uid)))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => p.Uid)))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.Uid)))
            .ForMember(dest => dest.Subscribers, opt => opt.MapFrom(src => src.Subscribers.Select(s => s.Uid)));
    }

    protected internal ExpositionModelProfile(string profileName) : base(profileName)
    {
    }
}