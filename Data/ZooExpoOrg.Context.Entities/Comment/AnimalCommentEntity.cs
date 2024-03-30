using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;
public class AnimalCommentEntity : Comment
{
    public int AnimalId { get; set; }
    public virtual AnimalEntity Animal { get; set; }
}
