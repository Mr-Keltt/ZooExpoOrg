using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class AnimalsSpeciesContextConfiguration
{
    public static void ConfigureAnimalsSpecies(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalSpecie>(entity =>
        {
            entity.ToTable("animals_species");

            entity.Property(ap => ap.Name).IsRequired();
        });
    }
}