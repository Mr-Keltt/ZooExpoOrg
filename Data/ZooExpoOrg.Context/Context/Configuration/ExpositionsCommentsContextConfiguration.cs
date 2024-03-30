using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ExpositionsCommentsContextConfiguration
{
    public static void ConfigureExpositionsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpositionCommentEntity>(entity =>
        {
            entity.ToTable("expositions_comments");

            entity.Property(с => с.Text).IsRequired();
            entity.Property(с => с.DateWriting).IsRequired();

            entity
                .HasOne(a => a.Exposition)
                .WithMany(a => a.Comments);
        });
    }
}