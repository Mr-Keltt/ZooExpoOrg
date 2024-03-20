using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AchievementEntity : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime DateAward { get; set; }

    public int AnimalId { get; set; }
    public virtual AnimalEntity Animal { get; set; }

    public int ConfirmationAchievementId { get; set; }

    public virtual ConfirmationAchievementEntity ConfirmationAchievement { get; set; }
}