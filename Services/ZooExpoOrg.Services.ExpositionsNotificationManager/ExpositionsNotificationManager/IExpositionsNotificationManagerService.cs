namespace ZooExpoOrg.Services.ExpositionsNotificationManager.ExpositionsNotificationManager;

public interface IExpositionsNotificationManagerService
{
    Task SendNotification(CreateNotificationModel model);
    Task CancelMailingByID(Guid id);
}