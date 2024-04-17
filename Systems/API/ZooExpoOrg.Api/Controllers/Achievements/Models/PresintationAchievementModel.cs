using AutoMapper;
using ZooExpoOrg.Services.Animals.Achievements;

namespace ZooExpoOrg.Api.Controllers.Achievements;

public class PresintationAchievementModel
{
    public Guid Id { get; set; }

    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class PresintationAchievementModelProfile : Profile
{
    public PresintationAchievementModelProfile()
    {
        CreateMap<AchievementModel, PresintationAchievementModel>();
    }
}