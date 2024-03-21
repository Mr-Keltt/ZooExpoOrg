using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Common.Enumerables;


namespace ZooExpoOrg.Context;

public static class UsersContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity
                .Property(e => e.Gender)
                .HasConversion<string>()
                .HasConversion(
                    e => e.ToString(),
                    e => (Gender)Enum.Parse(typeof(Gender), e)
                    )
                .IsRequired();

            entity
                .HasMany(e => e.OrganizedExpositions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.OrganizersId);

            entity
                .HasMany(e => e.Animals)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            entity
                .HasMany(e => e.Comments)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId);

            entity
                .HasOne(e => e.Photo)
                .WithOne(e => e.User)
                .HasPrincipalKey<UserEntity>(e => e.Id);
        });
    }
}