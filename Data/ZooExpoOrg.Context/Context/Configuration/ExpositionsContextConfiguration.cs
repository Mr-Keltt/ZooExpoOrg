using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ExpositionsContextConfiguration
{
    public static void ConfigureExpositions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exposition>(entity =>
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
                .HasMany(e => e.AnimalsSpecies)
                .WithOne()
                .HasForeignKey(e => e.Id);

            entity
                .HasMany(e => e.Participants)
                .WithOne()
                .HasForeignKey(p => p.Id);

            entity
                .HasMany(e => e.Photos)
                .WithOne(ph => ph.Exposition)
                .HasForeignKey(ph => ph.ExpositionId);

            entity
                .HasMany(e => e.Comments)
                .WithOne()
                .HasForeignKey(c => c.Id);

            entity
                .HasMany(e => e.Subscribers)
                .WithMany(u => u.Subscriptions)
                .UsingEntity(j => j.ToTable("exposition_subscribers"));
        });
    }
}