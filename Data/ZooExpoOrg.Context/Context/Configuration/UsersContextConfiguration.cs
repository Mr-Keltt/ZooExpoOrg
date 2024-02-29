using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context.Context;

public static class UsersContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity.Property(e => e.Gender).IsRequired();

            entity
                .HasOne(u => u.Photo)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PhotoId);

            entity
                .HasMany(u => u.OrganizedExpositions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.OrganizersId);

            entity
                .HasMany(u => u.Pets)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            entity
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.AuthorId);
        });
    }
}