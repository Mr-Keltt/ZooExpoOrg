using Asp.Versioning;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ZooExpoOrg.Api.Controllers.Animals.Animals;
using ZooExpoOrg.Api.Controllers.Clients;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.RightVerifier;

namespace ZooExpoOrg.Api.Controllers.Achievements;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Animals")]
[Route("v{version:apiVersion}/achievement")]
public class AchievementController : Controller
{
    private readonly IAppLogger logger;
    private readonly IAchievementService achievementService;
    private readonly IMapper mapper;
    private readonly IRightVerifierService rightVerifier;

    public AchievementController(
        IAppLogger logger,
        IAchievementService achievementService,
        IMapper mapper,
        IRightVerifierService rightVerifier
        )
    {
        this.logger = logger;
        this.achievementService = achievementService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
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
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            Guid jwtClientId = await rightVerifier.GetClientId(jwtToken);
            Guid requestClientId = await rightVerifier.GetClientIdByAnimalId(model.AnimalId);

            if (requestClientId != jwtClientId)
            {
                return BadRequest("Access denied.");
            }

            var achievements = await achievementService.Create(model);

            return Ok(mapper.Map<PresintationAchievementModel>(achievements));
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
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagAchievement(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            achievementService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
