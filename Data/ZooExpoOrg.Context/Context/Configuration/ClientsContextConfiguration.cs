using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Common.Enumerables;
using Microsoft.AspNetCore.Identity;

namespace ZooExpoOrg.Context;

public static class ClientsContextConfiguration
{
    public static void ConfigureClients(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientEntity>(entity =>
        {
            entity.ToTable("clients");

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
                .HasOne(e => e.User)
                .WithOne(e => e.Client)
                .HasPrincipalKey<ClientEntity>(e => e.Uid);

            entity
                .HasMany(e => e.OrganizedExpositions)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId);

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
                .WithOne(e => e.Owner)
                .HasPrincipalKey<ClientEntity>(e => e.Id);
        });
    }
}