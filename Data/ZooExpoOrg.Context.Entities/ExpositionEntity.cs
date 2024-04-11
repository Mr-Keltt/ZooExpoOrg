using AutoMapper;
using System.Runtime.CompilerServices;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ExpositionEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int OrganizerId { get; set; }
    public virtual ClientEntity Organizer { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<AnimalEntity> Participants { get; set; }

    public virtual ICollection<ExpositionPhotoEntity> Photos { get; set; }

    public virtual ICollection<ExpositionCommentEntity> Comments { get; set; }

    public virtual ICollection<ClientEntity> Subscribers { get; set; }
}