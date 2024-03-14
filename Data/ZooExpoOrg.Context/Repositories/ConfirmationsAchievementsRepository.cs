using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class ConfirmationsAchievementsRepository
{
    private readonly MainDbContext _dbContext;

    public ConfirmationsAchievementsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ConfirmationAchievement>> Get()
    {
        return await _dbContext.ConfirmationsAchievements
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ConfirmationAchievement?> GetById(int id)
    {
        return await _dbContext.ConfirmationsAchievements
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        string fileName,
        byte[] fileContent)
    {
        ConfirmationAchievement confirmationAchievement = new ConfirmationAchievement
        {
            FileContent = fileContent,
            FileName = fileName
        };

        await _dbContext.AddAsync(confirmationAchievement);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        int achievementId
        )
    {
        await _dbContext.ConfirmationsAchievements
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.AchievementId, achievementId)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.ConfirmationsAchievements
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
