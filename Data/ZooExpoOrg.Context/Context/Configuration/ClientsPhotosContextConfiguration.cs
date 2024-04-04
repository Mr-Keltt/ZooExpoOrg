using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ClientsPhotosContextConfiguration
{
    public static void ConfigureClientsPhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientPhotoEntity>(entity =>
        {
            entity.ToTable("clients_photos");

            entity.Property(ap => ap.ImageData).IsRequired();
        });
    }
}