using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ExpositionsContextConfiguration
{
    public static void ConfigureExpositions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpositionEntity>(entity =>
        {
            entity.ToTable("expositions");

            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Country).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.Street).IsRequired();
            entity.Property(e => e.DateStart).IsRequired();
            entity.Property(e => e.DateEnd).IsRequired();

            entity.Property(e => e.Description).HasMaxLength(10000);

            entity.Property(a => a.ParticipantsType)
                .HasConversion(
                    v => v.ToString(),
                    v => (AnimalType)Enum.Parse(typeof(AnimalType), v)
                    )
                .IsRequired();

            entity
                .HasMany(e => e.Participants)
                .WithMany(e => e.Expositions)
                .UsingEntity(j => j.ToTable("expositions_participants"));

            entity
                .HasMany(e => e.Photos)
                .WithOne(ph => ph.Exposition)
                .HasForeignKey(ph => ph.ExpositionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(p => p.Comments)
                .WithOne(c => c.Exposition)
                .HasForeignKey(c => c.ExpositionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(e => e.Subscribers)
                .WithMany(u => u.Subscriptions)
                .UsingEntity(j => j.ToTable("expositions_subscribers"));

            entity
                .HasMany(p => p.SentNotifications)
                .WithOne(c => c.Sender)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}