using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class AnimalPhotoEntity : BasePhoto
{
    public virtual AnimalEntity Owner { get; set; }
}
