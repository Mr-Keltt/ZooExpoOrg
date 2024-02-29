using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context.Context;

public static class UsersPhotosContextConfiguration
{
    public static void ConfigureUsersPhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPhoto>(entity =>
        {
            entity.ToTable("users_photos");

            entity.Property(ap => ap.ImageData).IsRequired();
        });
    }
}