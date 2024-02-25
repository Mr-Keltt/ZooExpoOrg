using System.Runtime.CompilerServices;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Exposition : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<AnimalSpecies> AnimalSpecies { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public ICollection<Pet>? Participants { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public ICollection<ExpoPhoto>? Photos { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public ICollection<User>? Subscribers { get; set; }
}
