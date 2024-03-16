using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;
public class AnimalComment : Comment
{
    public int AnimalId { get; set; }
    public virtual Animal Animal { get; set; }
}
