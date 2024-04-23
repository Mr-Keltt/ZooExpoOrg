namespace ZooExpoOrg.Web.Services.Achievements;

public class VueAchievementModel
{
    public Guid Id { get; set; }

    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class AchievementModelProfile : Profile
{
    public AchievementModelProfile()
    {
        CreateMap<AchievementEntity, VueAchievementModel>()
            .BeforeMap<AchievementModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore());
    }

    public class AchievementModelActions : IMappingAction<AchievementEntity, VueAchievementModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public AchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(AchievementEntity source, VueAchievementModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animal = db.Animals.FirstOrDefault(x => x.Id == source.AnimalId);

            if (animal == null)
            {
                throw new NullReferenceException();
            }

            destination.AnimalId = animal.Uid;
        }
    }
}