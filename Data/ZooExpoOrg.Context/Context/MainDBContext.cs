using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public class MainDbContext : DbContext
{
    public DbSet<AchievementEntity> Achievements { get; set; }
    public DbSet<AnimalEntity> Animals { get; set; }
    public DbSet<AnimalCommentEntity> AnimalsComments { get; set; }
    public DbSet<AnimalPhotoEntity> AnimalsPhotos { get; set; }
    public DbSet<ConfirmationAchievementEntity> ConfirmationsAchievements { get; set; }
    public DbSet<ExpositionEntity> Expositions { get; set; }
    public DbSet<ExpositionCommentEntity> ExpositionsComments { get; set; }
    public DbSet<ExpositionPhotoEntity> ExpositionsPhotos { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserPhotoEntity> UsersPhotos { get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAchievements();
        modelBuilder.ConfigureAnimals();
        modelBuilder.ConfigureAnimalsComments();
        modelBuilder.ConfigureAnimalsPhotos();
        modelBuilder.ConfigureConfirmationsAchievements();
        modelBuilder.ConfigureExpositionsComments();
        modelBuilder.ConfigureExpositionsPhotos();
        modelBuilder.ConfigureExpositions();
        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureUsersPhotos();
    }
}