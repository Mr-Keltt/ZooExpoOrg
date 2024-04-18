using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class PhotosContextConfiguration
{
    public static void ConfigurePhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoEntity>(entity =>
        {
            entity.ToTable("photos");

            entity.Property(ap => ap.ImageData).IsRequired();
            entity.Property(ap => ap.ImageMimeType).IsRequired();
        });
    }
}