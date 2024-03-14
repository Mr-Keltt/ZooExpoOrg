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
        int OwnerId,
        string Name,
        string? Description,
        string Breed,
        Gender Gender,
        DateTime BirthDate,
        int? Height,
        int? Weight,
        List<AnimalPhoto> Photos,
        List<Achievement>? Achievements)
    {
        Animal animal = new Animal
        {
            Name = Name,
            Description = Description,
            Breed = Breed,
            Gender = Gender,
            BirthDate = BirthDate,
            Height = Height,
            Weight = Weight,
            OwnerId = OwnerId,
            Photos = Photos,
            Achievements = Achievements
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
        List<Achievement>? achievements)
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
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.Animals
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
