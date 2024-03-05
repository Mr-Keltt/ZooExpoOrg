using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class PetsContextConfiguration
{
    public static void ConfigurePets(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.ToTable("pets");

            entity
                .HasOne(p => p.Animal)
                .WithMany()
                .HasForeignKey(p => p.AnimalId);

            entity
                .HasMany(e => e.Comments)
                .WithOne()
                .HasForeignKey(c => c.Id);
        });
    }
}