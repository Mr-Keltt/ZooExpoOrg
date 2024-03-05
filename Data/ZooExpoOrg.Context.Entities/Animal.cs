using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Animal : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public int AnimalSpecieId { get; set; }
    public virtual AnimalSpecie AnimalSpecie { get; set; }

    public string? Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<AnimalComment>? Comments { get; set; }

    public virtual ICollection<AnimalPhoto> Photos { get; set; }

    public virtual ICollection<Achievement>? Achievements { get; set; }
}
