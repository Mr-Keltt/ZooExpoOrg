using System.Runtime.CompilerServices;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Exposition : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int OrganizersId { get; set; }
    public virtual User User { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public virtual ICollection<Animal>? Participants { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<ExpositionPhoto>? Photos { get; set; }

    public virtual ICollection<ExpositionComment>? Comments { get; set; }

    public virtual ICollection<User>? Subscribers { get; set; }
}
