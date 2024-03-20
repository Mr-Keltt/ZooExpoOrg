using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities; 
public class UserPhotoEntity : BasePhoto
{ 
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; }
}
