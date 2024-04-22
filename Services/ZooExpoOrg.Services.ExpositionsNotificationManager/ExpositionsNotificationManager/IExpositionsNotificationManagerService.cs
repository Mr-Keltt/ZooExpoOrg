namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public interface IExpositionsNotificationManagerService
{
    Task SendNotification(Guid senderId, CreateNotificationModel model);
    Task MarkNotificationReaderByClientId(Guid clientId, Guid notificationId);
    Task CancelMailingByNotificationId(Guid notificationId);
}