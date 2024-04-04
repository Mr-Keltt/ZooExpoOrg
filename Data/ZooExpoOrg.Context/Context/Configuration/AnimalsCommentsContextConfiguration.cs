using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class AnimalsCommentsContextConfiguration
{
    public static void ConfigureAnimalsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalCommentEntity>(entity =>
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