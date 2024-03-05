using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context;

public static class ExpositionsCommentsContextConfiguration
{
    public static void ConfigureExpositionsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("expositions_comments");

            entity.Property(с => с.Text).IsRequired();
            entity.Property(с => с.DateWriting).IsRequired();
        });
    }
}