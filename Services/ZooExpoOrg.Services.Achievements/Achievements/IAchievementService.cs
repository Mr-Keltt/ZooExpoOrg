using ZooExpoOrg.Services.Achievements.Achievements.Models;

namespace ZooExpoOrg.Services.Achievements;

public interface IAchievementService
{
    public Task<IEnumerable<AchievementModel>> GetAllOwnedById(Guid OwnerId);
    Task<AchievementModel> GetById(Guid id);
    Task<AchievementModel> Create(CreateAchievementModel model);
    Task Update(Guid id, UpdateAchievementModel model);
    Task Delete(Guid id);
}