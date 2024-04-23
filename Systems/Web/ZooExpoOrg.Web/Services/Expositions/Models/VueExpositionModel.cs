using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Services.Expositions;

public class VueExpositionModel
{
    public Guid Id { get; set; }

    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public AnimalType ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<Guid> Participants { get; set; }

    public virtual ICollection<Guid> Photos { get; set; }

    public virtual ICollection<Guid> Comments { get; set; }

    public virtual ICollection<Guid> Subscribers { get; set; }

    public virtual ICollection<Guid> SentNotifications { get; set; }
}