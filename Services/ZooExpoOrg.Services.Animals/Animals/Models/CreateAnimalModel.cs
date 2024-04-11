using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ZooExpoOrg.Services.Animals;

public class CreateAnimalModel
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }
}

public class CreateAnimalModelProfile : Profile
{
    public CreateAnimalModelProfile()
    {
        CreateMap<CreateAnimalModel, AnimalEntity>()
            .BeforeMap<CreateAnimalModelActions>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class CreateAnimalModelActions : IMappingAction<CreateAnimalModel, AnimalEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAnimalModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(CreateAnimalModel source, AnimalEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Clients.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Id;     
            destination.Comments = new List<AnimalCommentEntity>();
            destination.Photos = new List<AnimalPhotoEntity>();
            destination.Achievements = new List<AchievementEntity>();
        }
    }
}