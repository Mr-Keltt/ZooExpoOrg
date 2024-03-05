using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class CommentsContextConfiguration
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comments");

            entity.Property(с => с.Text).IsRequired();
            entity.Property(с => с.DateWriting).IsRequired();
        });
    }
}