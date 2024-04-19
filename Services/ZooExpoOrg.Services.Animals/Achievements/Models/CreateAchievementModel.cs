using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Animals.Achievements;

public class CreateAchievementModel
{
    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class CreateAchievementModelProfile : Profile
{
    public CreateAchievementModelProfile()
    {
        CreateMap<CreateAchievementModel, AchievementEntity>()
            .BeforeMap<CreateAchievementModelActions>()
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore());
    }

    public class CreateAchievementModelActions : IMappingAction<CreateAchievementModel, AchievementEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateAchievementModel source, AchievementEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animal = db.Animals.FirstOrDefault(x => x.Uid == source.AnimalId);

            if (animal == null)
            {
                throw new NullReferenceException(); 
            }

            destination.AnimalId = animal.Id;
        }
    }
}