using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Achievements;

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
        CreateMap<AchievementEntity, CreateAchievementModel>()
            .BeforeMap<CreateAchievementModelActions>();
    }

    public class CreateAchievementModelActions : IMappingAction<AchievementEntity, CreateAchievementModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AchievementEntity source, CreateAchievementModel destination, ResolutionContext context)
        {

        }
    }
}