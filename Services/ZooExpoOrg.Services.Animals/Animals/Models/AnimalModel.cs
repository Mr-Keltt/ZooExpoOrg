namespace ZooExpoOrg.Services.Animals.Animals;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;

public class AnimalModel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public virtual IEnumerable<Guid> Comments { get; set; }

    public virtual IEnumerable<Guid> Photos { get; set; }

    public virtual IEnumerable<Guid> Achievements { get; set; }

    public virtual ICollection<Guid> Expositions { get; set; }
}


public class AnimalModelProfile : Profile
{
    public AnimalModelProfile()
    {
        CreateMap<AnimalEntity, AnimalModel>()
            .BeforeMap<AnimalModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(e => e.Uid)))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(e => e.Uid)))
            .ForMember(dest => dest.Achievements, opt => opt.MapFrom(src => src.Achievements.Select(e => e.Uid)))
            .ForMember(dest => dest.Expositions, opt => opt.MapFrom(src => src.Expositions.Select(e => e.Uid)))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class AnimalModelActions : IMappingAction<AnimalEntity, AnimalModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public AnimalModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AnimalEntity source, AnimalModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Clients.FirstOrDefaultAsync(x => x.Id == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Uid;
        }
    }
}