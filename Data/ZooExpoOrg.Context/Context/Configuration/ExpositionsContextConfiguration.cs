using Microsoft.EntityFrameworkCore;
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
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Country).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.Street).IsRequired();
            entity.Property(e => e.DateStart).IsRequired();
            entity.Property(e => e.DateEnd).IsRequired();

            entity
                .HasMany(e => e.Participants)
                .WithMany(e => e.Expositions)
                .UsingEntity(j => j.ToTable("expositions_participants"));

            entity
                .HasMany(e => e.Photos)
                .WithOne(ph => ph.Owner)
                .HasForeignKey(ph => ph.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(p => p.Comments)
                .WithOne(c => c.Exposition)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(e => e.Subscribers)
                .WithMany(u => u.Subscriptions)
                .UsingEntity(j => j.ToTable("expositions_subscribers"));
        });
    }
}