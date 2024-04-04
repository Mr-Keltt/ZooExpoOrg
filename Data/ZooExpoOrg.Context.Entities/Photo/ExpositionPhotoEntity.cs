using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ExpositionPhotoEntity : BasePhoto
{
    public virtual ExpositionEntity Owner { get; set; }
}
