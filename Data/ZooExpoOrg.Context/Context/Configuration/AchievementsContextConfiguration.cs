using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context.Context;

public static class AchievementsContextConfiguration
{
    public static void ConfigureAchievements(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.ToTable("achievements");
            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.DateAward).IsRequired();

            entity
                .HasOne(a => a.ConfirmationAchievement)
                .WithOne(ca => ca.Achievement)
                .HasForeignKey<ConfirmationAchievement>(c => c.AchievementId);
        })
    }
}