using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context;

public static class AnimalsCommentsContextConfiguration
{
    public static void ConfigureAnimalsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("animals_comments");

            entity.Property(с => с.Text).IsRequired();
            entity.Property(с => с.DateWriting).IsRequired();
        });
    }
}