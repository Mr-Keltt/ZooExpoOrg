using Microsoft.EntityFrameworkCore;
using System;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Common.Enumerables;

namespace ZooExpoOrg.Context;

public static class AnimalsContextConfiguration
{
    public static void ConfigureAnimals(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalEntity>(entity =>
        {
            entity.ToTable("animals");

            entity.Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.BirthDate).IsRequired();
            entity.Property(a => a.OwnerId).IsRequired();

            entity.Property(a => a.Description).HasMaxLength(10000);

            entity.Property(a => a.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v)
                    )
                .IsRequired();

            entity.Property(a => a.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (AnimalType)Enum.Parse(typeof(AnimalType), v)
                    )
                .IsRequired();

            entity
                .HasMany(a => a.Photos)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.Achievements)
                .WithOne(a => a.Animal)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.Comments)
                .WithOne(a => a.Animal)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}