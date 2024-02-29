using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ConfirmationAchievement : PdfFile
{
    public int AchievementId { get; set; }
    public virtual Achievement Achievement { get; set; }
}

