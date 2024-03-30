namespace ZooExpoOrg.Context.Entities;

using ZooExpoOrg.Context.Entities.Common;

public class ExpositionCommentEntity : Comment
{
    public int ExpositionId { get; set; }
    public virtual ExpositionEntity Exposition { get; set; }
}