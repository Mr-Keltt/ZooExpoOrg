using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class NotificationEntity : BaseEntity
{
    public Guid MailingID { get; set; }

    public int SenderNotificationsId { get; set; }
    public virtual ExpositionEntity SenderNotification { get; set; }

    public virtual ICollection<ClientEntity> RecipientsNotification { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public DateTime DepartureTime { get; set; }
}

