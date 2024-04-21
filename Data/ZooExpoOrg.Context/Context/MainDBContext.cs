using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public class MainDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public DbSet<AchievementEntity> Achievements { get; set; }
    public DbSet<AnimalEntity> Animals { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<ExpositionEntity> Expositions { get; set; }
    public DbSet<PhotoEntity> Photos { get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAchievements();
        modelBuilder.ConfigureAnimals();
        modelBuilder.ConfigureClients();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureNotifications();
        modelBuilder.ConfigureExpositions();
        modelBuilder.ConfigurePhotos();
        modelBuilder.ConfigureUsers();
    }
}