using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context;

public static class AnimalsCommentsContextConfiguration
{
    public static void ConfigureAnimalsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalComment>(entity =>
        {
            entity.ToTable("animals_comments");

            entity.Property(с => с.Text).IsRequired();
            entity.Property(с => с.DateWriting).IsRequired();

            entity
                .HasOne(a => a.Animal)
                .WithMany(a => a.Comments);
        });
    }
}