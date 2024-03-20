using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ConfirmationAchievementEntity : PdfFile
{
    public int? AchievementId { get; set; }
    public virtual AchievementEntity Achievement { get; set; }
}

