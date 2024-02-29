using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Achievement : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime DateAward { get; set; }

    public int AnimalId { get; set; }
    public virtual Animal Animal { get; set; }

    public int ConfirmationAchievementId { get; set; }

    public virtual ConfirmationAchievement ConfirmationAchievement { get; set; }
}