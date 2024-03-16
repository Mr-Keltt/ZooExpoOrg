using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context;

public class AnimalsRepository
{
    private readonly MainDbContext _dbContext;

    public AnimalsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Animal>> Get()
    {
        return await _dbContext.Animals
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Animal?> GetById(int id)
    {
        return await _dbContext.Animals
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int ownerId,
        string name,
        string? description,
        string breed,
        Gender gender,
        DateTime birthDate,
        int? height,
        int? weight,
        List<AnimalPhoto> photos,
        List<Achievement>? achievements)
    {
        Animal animal = new Animal
        {
            Name = name,
            Description = description,
            Breed = breed,
            Gender = gender,
            BirthDate = birthDate,
            Height = height,
            Weight = weight,
            OwnerId = ownerId,
            Photos = photos,
            Achievements = achievements
        };

        await _dbContext.AddAsync(animal);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string name,
        string? description,
        string breed,
        Gender gender,
        DateTime birthDate,
        int? height,
        int? weight,
        int ownerId,
        List<AnimalPhoto> photos,
        List<Achievement>? achievements,
        List<AnimalComment>? comments)
    {
        await _dbContext.Animals
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Name, name)
                .SetProperty(a => a.Description, description)
                .SetProperty(a => a.Breed, breed)
                .SetProperty(a => a.Gender, gender)
                .SetProperty(a => a.BirthDate, birthDate)
                .SetProperty(a => a.Height, height)
                .SetProperty(a => a.Weight, weight)
                .SetProperty(a => a.OwnerId, ownerId)
                .SetProperty(a => a.Photos, photos)
                .SetProperty(a => a.Achievements, achievements)
                .SetProperty(a => a.Comments, comments)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.Animals
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
