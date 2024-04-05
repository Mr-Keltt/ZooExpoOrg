namespace ZooExpoOrg.Context.Entities;

public class AnimalCommentEntity : CommentEntity
{
    public int AnimalId { get; set; }
    public virtual AnimalEntity Animal { get; set; }
}
