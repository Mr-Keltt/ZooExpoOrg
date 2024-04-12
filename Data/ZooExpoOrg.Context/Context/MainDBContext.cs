using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context;

public class MainDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public DbSet<AchievementEntity> Achievements { get; set; }
    public DbSet<AnimalEntity> Animals { get; set; }
    public DbSet<AnimalCommentEntity> AnimalsComments { get; set; }
    public DbSet<AnimalPhotoEntity> AnimalsPhotos { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ClientPhotoEntity> ClientsPhotos { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<ExpositionEntity> Expositions { get; set; }
    public DbSet<ExpositionCommentEntity> ExpositionsComments { get; set; }
    public DbSet<ExpositionPhotoEntity> ExpositionsPhotos { get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAchievements();
        modelBuilder.ConfigureAnimals();
        modelBuilder.ConfigureAnimalsComments();
        modelBuilder.ConfigureAnimalsPhotos();
        modelBuilder.ConfigureClients();
        modelBuilder.ConfigureClientsPhotos();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureExpositionsComments();
        modelBuilder.ConfigureExpositionsPhotos();
        modelBuilder.ConfigureExpositions();
        modelBuilder.ConfigureUsers();
    }
}