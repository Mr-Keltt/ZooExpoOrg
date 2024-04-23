using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Animals.Achievements;

public class AchievementService : IAchievementService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IModelValidator<CreateAchievementModel> createAchievementModelValidator;

    public AchievementService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAppLogger logger,
        IModelValidator<CreateAchievementModel> createAchievementModelValidator
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
        this.createAchievementModelValidator = createAchievementModelValidator;
    }

    public async Task<IEnumerable<AchievementModel>> GetAllOwnedById(Guid ownerId)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == ownerId);

        if (animal == null)
        {
            throw new ProcessException($"Animal (ID={ownerId}) not found.");
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
        createAchievementModelValidator.Check(model);

        using var db = dbContextFactory.CreateDbContext();

        var animal = db.Animals.FirstOrDefault(x => x.Uid == model.AnimalId);

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
        using var db = await dbContextFactory.CreateDbContextAsync();

        var achievement = await db.Achievements.FirstOrDefaultAsync(x => x.Uid == id);

        if (achievement == null)
        {
            throw new ProcessException($"Achievement (ID={id}) not found.");
        }

        db.Achievements.Remove(achievement);

        await db.SaveChangesAsync();
    }
}