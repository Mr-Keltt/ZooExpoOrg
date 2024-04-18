using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Animals.Achievements;

public class AchievementService : IAchievementService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public AchievementService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<AchievementModel>> GetAllOwnedById(Guid OwnerId)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == OwnerId);

        if (animal == null)
        {
            throw new ProcessException($"Animal (ID={OwnerId}) not found.");
        }
        
        return mapper.Map<IEnumerable<AchievementModel>>(animal.Achievements);
    }

    public async Task<AchievementModel> GetById(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var achievement = await db.Achievements.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<AchievementModel>(achievement);
    }

    public async Task<AchievementModel> Create(CreateAchievementModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == model.AnimalId);

        if (animal == null)
        {
            throw new ProcessException($"Animal (ID={model.AnimalId}) not found.");
        }

        var achievement = mapper.Map<AchievementEntity>(model);

        db.Achievements.Add(achievement);

        animal.Achievements.Add(achievement);

        db.SaveChanges();

        return mapper.Map<AchievementModel>(achievement);
    }

    public async Task Delete(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var achievement = await db.Achievements.FirstOrDefaultAsync(x => x.Uid == id);

        if (achievement == null)
        {
            throw new ProcessException($"Achievement (ID={id}) not found.");
        }

        db.Achievements.Remove(achievement);

        await db.SaveChangesAsync();
    }
}