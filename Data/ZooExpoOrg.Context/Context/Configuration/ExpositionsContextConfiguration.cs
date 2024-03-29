﻿using Microsoft.EntityFrameworkCore;
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
                .WithOne()
                .HasForeignKey(p => p.Id);

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
        });
    }
}