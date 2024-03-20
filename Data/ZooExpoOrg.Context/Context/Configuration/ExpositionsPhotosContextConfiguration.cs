using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ExpositionsPhotosContextConfiguration
{
    public static void ConfigureExpositionsPhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpositionPhotoEntity>(entity =>
        {
            entity.ToTable("expositions_photos");

            entity.Property(ap => ap.ImageData).IsRequired();
        });
    }
}