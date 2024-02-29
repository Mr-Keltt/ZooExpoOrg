using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Context.Context;

public class MainDbContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalPhoto> AnimalsPhotos { get; set; }
    public DbSet<AnimalSpecie> AnimalsSpecies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ConfirmationAchievement> ConfirmationsAchievements { get; set; }
    public DbSet<ExpositionPhoto> ExpositionsPhotos { get; set; }
    public DbSet<Exposition> Expositions { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPhoto> UsersPhotos { get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAchievements();
        modelBuilder.ConfigureAnimals();
        modelBuilder.ConfigureAnimalsPhotos();
        modelBuilder.ConfigureAnimalsSpecies();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureConfirmationsAchievements();
        modelBuilder.ConfigureExpositionsPhotos();
        modelBuilder.ConfigureExpositions();
        modelBuilder.ConfigurePets();
        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureUsersPhotos();
    }
}