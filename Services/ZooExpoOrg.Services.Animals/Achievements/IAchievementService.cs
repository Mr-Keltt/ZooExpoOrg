namespace ZooExpoOrg.Services.Animals.Achievements;

public interface IAchievementService
{
    Task<IEnumerable<AchievementModel>> GetAllOwnedById(Guid ownerId);
    Task<AchievementModel> GetById(Guid id);
    Task<AchievementModel> Create(CreateAchievementModel model);
    Task Delete(Guid id);
}