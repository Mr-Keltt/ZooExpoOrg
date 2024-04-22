using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public class ExpositionsNotificationManagerService : IExpositionsNotificationManagerService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public ExpositionsNotificationManagerService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task SendNotification(Guid senderId, CreateNotificationModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

        var sender = db.Expositions.FirstOrDefault(x => x.Uid == senderId);

        if (sender == null) 
        {
            throw new ProcessException($"Exposition (ID = {senderId}) not found.");
        }

        var notification = mapper.Map<NotificationEntity>(model);

        notification.SenderId = sender.Id;
        notification.Recipients = sender.Subscribers;

        foreach (ClientEntity client in notification.Recipients)
        {
            client.UnreadNotifications.Add(notification);
            sender.SentNotifications.Add(notification);
            db.Notifications.Add(notification);
        }

        db.SaveChanges();
    }

    public Task MarkNotificationReaderByClientId(Guid notificationId, Guid clientId)
    {
        throw new NotImplementedException();
    }

    public async Task CancelMailingByNotificationId(Guid notificationId)
    {
        using var db = dbContextFactory.CreateDbContext();

        var notification = db.Notifications
            .Include(x => x.Sender)
            .Include(x => x.Recipients)
            .FirstOrDefault(x => x.Uid == notificationId);

        if (notification == null)
        {
            throw new ProcessException($"Notification (ID = {notificationId}) not found.");
        }

        foreach (ClientEntity client in notification.Recipients)
        {
            client.UnreadNotifications.Remove(notification);
        }

        notification.Sender.SentNotifications.Remove(notification);

        db.Notifications.Remove(notification);

        db.SaveChanges();
    }
}