using AutoMapper;
using System.Runtime.CompilerServices;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ExpositionEntity : BaseEntity
{
    public int OrganizerId { get; set; }
    public virtual ClientEntity Organizer { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public AnimalType ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<AnimalEntity> Participants { get; set; }

    public virtual ICollection<PhotoEntity> Photos { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; }

    public virtual ICollection<ClientEntity> Subscribers { get; set; }

    public virtual ICollection<NotificationEntity> SentNotifications { get; set; }
}