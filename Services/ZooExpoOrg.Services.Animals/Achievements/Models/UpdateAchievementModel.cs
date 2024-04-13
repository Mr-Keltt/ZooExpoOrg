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
        CreateMap<UpdateAchievementModel, AchievementEntity>();
    }   
}