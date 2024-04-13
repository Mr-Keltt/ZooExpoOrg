using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Api.Controllers.Animals.Achievement;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Animals")]
[Route("v{version:apiVersion}/achievement")]
public class AchievementController : Controller
{
    private readonly IAppLogger logger;
    private readonly IAchievementService achievementService;
    private readonly IMapper mapper;

    public AchievementController(
        IAppLogger logger, 
        IAchievementService achievementService, 
        IMapper mapper
        )
    {
        this.logger = logger;
        this.achievementService = achievementService;
        this.mapper = mapper;
    }

    [HttpGet("owned/{ownedId:Guid}")]
    public async Task<IEnumerable<AchievementModel>> GetAllOwned(Guid OwnerId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:Guid}")]
    public async Task<AchievementModel> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("")]
    public async Task<AchievementModel> Create(CreateAchievementModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:Guid}")]
    public async Task Update(Guid id, UpdateAchievementModel model)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
