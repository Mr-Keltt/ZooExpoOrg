namespace ZooExpoOrg.Web.Services.Animals;

using ZooExpoOrg.Web.Common.Enumerables;

public class VueAnimalModel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AnimalType Type { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public virtual IEnumerable<Guid> Comments { get; set; }

    public virtual IEnumerable<Guid> Photos { get; set; }

    public virtual IEnumerable<Guid> Achievements { get; set; }

    public virtual ICollection<Guid> Expositions { get; set; }
}