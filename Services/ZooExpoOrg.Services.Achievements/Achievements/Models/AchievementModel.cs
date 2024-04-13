using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Achievements;

public class AchievementModel
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
        CreateMap<AchievementEntity, AchievementModel>()
            .BeforeMap<AchievementModelActions>();
    }

    public class AchievementModelActions : IMappingAction<AchievementEntity, AchievementModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public AchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AchievementEntity source, AchievementModel destination, ResolutionContext context)
        {
            
        }
    }
}