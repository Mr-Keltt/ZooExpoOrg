using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class AnimalsPhotosContextConfiguration
{
    public static void ConfigureAnimalsPhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalPhoto>(entity =>
        {
            entity.ToTable("animals_photos");

            entity.Property(ap => ap.ImageData).IsRequired();
        });
    }
}