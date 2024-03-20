using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;
public class ExpositionCommentEntity : Comment
{
    public int ExpositionId { get; set; }
    public virtual ExpositionEntity Exposition { get; set; }
}