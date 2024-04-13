using AutoMapper;
using ZooExpoOrg.Api.Controllers.Animals.Animal;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;

namespace ZooExpoOrg.Api.Controllers.Animals.Achievement;

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