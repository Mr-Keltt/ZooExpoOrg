using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class AchievementsRepository
{
    private readonly MainDbContext _dbContext;

    public AchievementsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Achievement>> Get()
    {
        return await _dbContext.Achievements
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Achievement?> GetById(int id)
    {
        return await _dbContext.Achievements
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int animalId,
        int confirmationAchievementId,
        string name,
        string? description,
        DateTime dateAward)
    {
        Achievement achievement = new Achievement
        {
            Name = name,
            Description = description,
            DateAward = dateAward,
            AnimalId = animalId,
            ConfirmationAchievementId = confirmationAchievementId
        };

        await _dbContext.AddAsync(achievement);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string name,
        string? description,
        DateTime dateAward,
        int animalId,
        int confirmationAchievementId)
    {
        await _dbContext.Achievements
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Name, name)
                .SetProperty(a => a.Description, description)
                .SetProperty(a => a.DateAward, dateAward)
                .SetProperty(a => a.AnimalId, animalId)
                .SetProperty(a => a.ConfirmationAchievementId, confirmationAchievementId)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.Achievements
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
