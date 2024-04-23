using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Services.Animals;

public class VueCreateAnimalModel
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AnimalType Type { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }
}