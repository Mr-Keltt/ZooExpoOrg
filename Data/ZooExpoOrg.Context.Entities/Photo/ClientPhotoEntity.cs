using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;
public class ClientPhotoEntity : BasePhoto
{
    public virtual ClientEntity Owner { get; set; }
}
