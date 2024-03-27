namespace ZooExpoOrg.Services.Animals;

using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;
using ZooExpoOrg.Services.Photos;

public class AnimalModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public Guid OwnerId { get; set; }

    //public virtual IEnumerable<CommentModel> Comments { get; set; }

    public virtual IEnumerable<PhotoModel> Photos { get; set; }

    //public virtual IEnumerable<AchievementModel> Achievements { get; set; }
}


public class AnimalModelProfile : Profile
{
    public AnimalModelProfile()
    {
        CreateMap<AnimalEntity, AnimalModel>()
            .BeforeMap<AnimalModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
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

            var animal = await db.Animals
                .Include(x => x.Owner).ThenInclude(x => x.Photo)
                .Include(x => x.Comments).ThenInclude(x => x.Author)
                .Include(x => x.Photos)
                .Include(x => x.Achievements).ThenInclude(x => x.ConfirmationAchievement)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = animal.Uid;
            destination.Description = !animal.Description.IsNullOrEmpty() ? animal.Description : "";
            destination.OwnerId = animal.Owner.Uid;
        }
    }
}