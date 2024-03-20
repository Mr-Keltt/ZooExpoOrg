using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class UsersPhotosContextConfiguration
{
    public static void ConfigureUsersPhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPhotoEntity>(entity =>
        {
            entity.ToTable("users_photos");

            entity.Property(ap => ap.ImageData).IsRequired();
        });
    }
}