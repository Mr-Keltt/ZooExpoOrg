using AutoMapper;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class NotificationEntity : BaseEntity
{
    public int SenderId { get; set; }
    public virtual ExpositionEntity Sender { get; set; }

    public virtual ICollection<ClientEntity> Recipients { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public DateTime DepartureTime { get; set; }
}