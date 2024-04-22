namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public interface IExpositionsNotificationManagerService
{
    Task SendNotification(Guid senderId, CreateNotificationModel model);
    Task MarkNotificationReaderByClientId(Guid notificationId, Guid clientId);
    Task CancelMailingByNotificationId(Guid notificationId);
}