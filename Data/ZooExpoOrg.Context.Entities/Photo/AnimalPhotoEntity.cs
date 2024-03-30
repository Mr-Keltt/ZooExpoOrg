using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AnimalPhotoEntity : BasePhoto
{
    public int AnimalId { get; set; }
    public virtual AnimalEntity Animal { get; set; }
}
