using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Achievements.Achievements;

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
}