using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ExpositionPhotoEntity : BasePhoto
{
    public int ExpositionId { get; set; }
    public virtual ExpositionEntity Exposition { get; set; }
}
