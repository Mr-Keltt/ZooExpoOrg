using ZooExpoOrg.Common.Enumerables;

namespace ZooExpoOrg.Services.Animals;

public class UpdateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public Guid OwnerId { get; set; }

    //public virtual IEnumerable<PhotoModel> Photos { get; set; }

    //public virtual IEnumerable<AchievementModel> Achievements { get; set; }
}
