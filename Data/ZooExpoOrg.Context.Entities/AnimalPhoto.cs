using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AnimalPhoto : BasePhoto
{
    public int AnimalId { get; set; }
    public virtual Animal Animal { get; set; }
}
