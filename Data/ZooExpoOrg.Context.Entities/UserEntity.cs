using Microsoft.AspNetCore.Identity;

namespace ZooExpoOrg.Context.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public Guid ClientId { get; set; }
    public virtual ClientEntity Client { get; set; }
}