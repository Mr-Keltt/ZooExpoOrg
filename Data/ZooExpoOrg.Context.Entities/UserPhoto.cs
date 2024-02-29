using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities; 
public class UserPhoto : BasePhoto
{ 
    public int UserId { get; set; }
    public virtual User User { get; set; }
}
