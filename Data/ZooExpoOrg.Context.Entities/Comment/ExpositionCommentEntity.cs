namespace ZooExpoOrg.Context.Entities;

using ZooExpoOrg.Context.Entities.Common;

public class ExpositionCommentEntity : CommentEntity
{
    public int ExpositionId { get; set; }
    public virtual ExpositionEntity Exposition { get; set; }
}