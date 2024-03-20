namespace ZooExpoOrg.Context.Entities.Common;

public class Comment : BaseEntity
{
    public int AuthorId { get; set; }
    public virtual UserEntity Author { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}
