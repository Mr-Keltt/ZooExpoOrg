using Microsoft.EntityFrameworkCore;
using System;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Context;

public static class AnimalsContextConfiguration
{
    public static void ConfigureAnimals(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.ToTable("animals");

            entity.Property(a => a.Name).IsRequired();
            entity
                .Property(a => a.Gender)
                .HasConversion<string>()
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v)
                    )
                .IsRequired();
            entity.Property(a => a.BirthDate).IsRequired();
            entity.Property(a => a.Name).IsRequired();


            entity
                .HasMany(a => a.Photos)
                .WithOne(p => p.Animal)
                .HasForeignKey(p => p.AnimalId);

            entity
                .HasMany(a => a.Achievements)
                .WithOne(ach => ach.Animal)
                .HasForeignKey(ach => ach.AnimalId);

            entity
                .HasOne(a => a.Mother)
                .WithMany()
                .HasForeignKey(a => a.MotherId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(a => a.Father)
                .WithMany()
                .HasForeignKey(a => a.FatherId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}