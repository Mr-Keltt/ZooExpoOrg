using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ClientEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual UserEntity User { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalEntity> Animals { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; }

    public virtual ICollection<PhotoEntity> Photos { get; set; }

    public virtual ICollection<PhotoEntity> OwnedPhotos { get; set; }

    public virtual ICollection<NotificationEntity> UnreadNotifications { get; set; }
}
