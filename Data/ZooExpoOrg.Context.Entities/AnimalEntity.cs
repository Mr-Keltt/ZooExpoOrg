using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AnimalEntity : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int OwnerId { get; set; }
    public virtual UserEntity User { get; set; }

    public virtual ICollection<AnimalCommentEntity> Comments { get; set; }

    public virtual ICollection<AnimalPhotoEntity> Photos { get; set; }

    public virtual ICollection<AchievementEntity> Achievements { get; set; }
}
