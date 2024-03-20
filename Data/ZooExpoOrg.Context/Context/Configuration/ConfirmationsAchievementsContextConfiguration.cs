using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public static class ConfirmationsAchievementsContextConfiguration
{
    public static void ConfigureConfirmationsAchievements(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfirmationAchievementEntity>(entity =>
        {
            entity.ToTable("confirmations_achievements");

            entity
                .HasOne(a => a.Achievement)
                .WithOne(ca => ca.ConfirmationAchievement)
                .HasForeignKey<AchievementEntity>(a => a.ConfirmationAchievementId);
        });
    }
}