namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public interface IExpositionsNotificationManagerService
{
    Task<NotificationModel> GetAllNotificationsReceivedById(Guid recipientId);
    Task SendNotification(Guid senderId, CreateNotificationModel model);
    Task MarkNotificationReaderByClientId(Guid clientId, Guid notificationId);
    Task CancelMailingByNotificationId(Guid notificationId);
}