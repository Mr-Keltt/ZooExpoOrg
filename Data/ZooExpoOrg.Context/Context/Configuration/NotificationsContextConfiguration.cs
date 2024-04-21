using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class NotificationsContextConfiguration
{
    public static void ConfigureNotifications(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationEntity>(entity =>
        {
            entity.ToTable("notifications");

            entity.Property(e => e.MailingID).IsRequired();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Text).HasMaxLength(10000).IsRequired();
            entity.Property(e => e.DepartureTime).IsRequired();
        });
    }
}