namespace ZooExpoOrg.Web.Services.Achievements;

public interface IAchievementService
{
    Task<IEnumerable<VueAchievementModel>> GetAchievementsOwned(Guid ownerId);
    Task<VueAchievementModel> GetAchievement(Guid achievementId);
    Task AddAchievement(VueCreateAchievementModel model);
    Task DeleteAchievement(Guid achievementId);
}