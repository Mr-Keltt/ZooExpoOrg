using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Achievement : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }

    public ConfirmationAchievement confirmationAchievement { get; set; }
}