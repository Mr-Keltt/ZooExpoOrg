using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Animals.Achievements;

public class UpdateAchievementModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class UpdateAchievementModelProfile : Profile
{
    public UpdateAchievementModelProfile()
    {
        CreateMap<AchievementEntity, UpdateAchievementModel>()
            .BeforeMap<UpdateAchievementModelActions>();
    }

    public class UpdateAchievementModelActions : IMappingAction<AchievementEntity, UpdateAchievementModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateAchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AchievementEntity source, UpdateAchievementModel destination, ResolutionContext context)
        {

        }
    }
}