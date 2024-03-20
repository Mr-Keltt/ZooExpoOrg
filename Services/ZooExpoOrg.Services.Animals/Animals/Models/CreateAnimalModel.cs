using ZooExpoOrg.Common.Enumerables;

namespace ZooExpoOrg.Services.Animals;

public class CreateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public Guid OwnerId { get; set; }

    //public virtual IEnumerable<PhotoModel> Photos { get; set; }
}
