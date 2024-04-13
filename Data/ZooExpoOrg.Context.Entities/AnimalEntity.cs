using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AnimalEntity : BaseEntity
{
    public int OwnerId { get; set; }
    public virtual ClientEntity Owner { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }
    
    public int? Height { get; set; }

    public int? Weight { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; }

    public virtual ICollection<AnimalPhotoEntity> Photos { get; set; }

    public virtual ICollection<AchievementEntity> Achievements { get; set; }

    public virtual ICollection<ExpositionEntity> Expositions { get; set; }
}
