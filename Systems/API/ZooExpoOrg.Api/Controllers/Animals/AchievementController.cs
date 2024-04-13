using Asp.Versioning;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Api.Controllers.Animals.Animals;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Clients;
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

    [HttpGet("owned/{ownerId:Guid}")]
    public async Task<IActionResult> GetAllOwned(Guid ownerId)
    {
        try
        {
            var achievements = await achievementService.GetAllOwnedById(ownerId);

            if (achievements == null)
            {
                return NotFound($"Achievements not found.");
            }

            return Ok(mapper.Map<IEnumerable<PresintationAchievementModel>>(achievements));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var achievements = await achievementService.GetById(id);

        if (achievements == null)
        {
            return NotFound($"Achievements not found.");
        }

        return Ok(mapper.Map<PresintationAchievementModel>(achievements));
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateAchievementModel model)
    {
        try
        {
            var achievements = await achievementService.Create(model);

            return Ok(mapper.Map<PresintationAchievementModel>(achievements));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAchievementModel model)
    {
        try
        {
            await achievementService.Update(id, model);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            achievementService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
