using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Achievements;

public interface IAchievementService
{
    Task<GetModelResult<List<VueAchievementModel>>> GetAchievementsOwned(Guid ownerId);
    Task<GetModelResult<VueAchievementModel>> GetAchievement(Guid achievementId);
    Task<ManageModelResult<VueAchievementModel>> AddAchievement(VueCreateAchievementModel model);
    Task<DeleteModelResult> DeleteAchievement(Guid achievementId);
}