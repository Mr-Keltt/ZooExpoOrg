using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class CommentEntity : BaseEntity
{
    public int AuthorId { get; set; }
    public virtual ClientEntity Author { get; set; }

    public int? ExpositionId { get; set; }
    public virtual ExpositionEntity? Exposition { get; set; }

    public int? AnimalId { get; set; }
    public virtual AnimalEntity? Animal { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}
