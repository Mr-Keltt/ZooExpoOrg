using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class CommentEntity : BaseEntity
{
    public int AuthorId { get; set; }
    public virtual ClientEntity Author { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}
