using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class AchievementsContextConfiguration
{
    public static void ConfigureAchievements(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AchievementEntity>(entity =>
        {
            entity.ToTable("achievements");
            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.DateAward).IsRequired();
        });
    }
}