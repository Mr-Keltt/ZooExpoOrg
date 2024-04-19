using AutoMapper;
using ZooExpoOrg.Services.Animals.Achievements;

namespace ZooExpoOrg.Api.Controllers.Achievements;

public class PresintationCreateAchievementModel
{
    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class PresintationCreateAchievementModelProfile : Profile
{
    public PresintationCreateAchievementModelProfile()
    {
        CreateMap<PresintationAchievementModel, CreateAchievementModel>();
    }
}