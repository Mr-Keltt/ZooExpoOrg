using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
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

    public Task<IEnumerable<AchievementModel>> GetAllOwnedById(Guid OwnerId)
    {
        throw new NotImplementedException();
    }

    public Task<AchievementModel> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<AchievementModel> Create(CreateAchievementModel model)
    {
        throw new NotImplementedException();
    }

    public Task Update(Guid id, UpdateAchievementModel model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}