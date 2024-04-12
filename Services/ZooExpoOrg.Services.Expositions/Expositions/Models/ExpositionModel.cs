using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
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
            .BeforeMap<ExpositionModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart))
            .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd))
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.Select(p => p.Uid)))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => p.Uid)))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.Uid)))
            .ForMember(dest => dest.Subscribers, opt => opt.MapFrom(src => src.Subscribers.Select(s => s.Uid)))
            .ForMember(dest => dest.OrganizerId, opt => opt.Ignore());
    }

    public class ExpositionModelActions : IMappingAction<ExpositionEntity, ExpositionModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public ExpositionModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(ExpositionEntity source, ExpositionModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var organizer = await db.Clients.FirstOrDefaultAsync(x => x.Id == source.OrganizerId);

            if (organizer == null)
            {
                throw new NullReferenceException();
            }

            destination.OrganizerId = organizer.Uid;
        }
    }
}