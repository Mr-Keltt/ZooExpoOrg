using Microsoft.AspNetCore.Identity;

namespace ZooExpoOrg.Context.Entities;

public class ApiUser : IdentityUser<Guid>
{
    public ApiUserStatus Status { get; set; }
}
