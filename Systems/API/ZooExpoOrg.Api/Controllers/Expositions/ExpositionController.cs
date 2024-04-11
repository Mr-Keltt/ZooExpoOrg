using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Expositions;

namespace ZooExpoOrg.Api.Controllers.Expositions;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Expositions")]
[Route("v{version:apiVersion}/exposition")]
public class ExpositionController : Controller
{
    [HttpGet("")]
    public Task<IActionResult> GetAll()
    {

    }

    [HttpGet("{id:Guid}")]
    public Task<IActionResult> GetById(Guid id)
    {

    }

    [HttpPost("")]
    public Task<PresintationExpositionModel> Create(CreateExpositionModel model)
    {

    }

    [HttpPut("{id:Guid}")]
    public Task Update(Guid id, UpdateExpositionModel model)
    {

    }

    [HttpGet("")]
    public Task<IActionResult> GetAllSubscribers()
    {

    }

    [HttpPut("{id:Guid}")]
    public Task Subscribe(Guid userId)
    {

    }

    [HttpDelete("{id:Guid}")]
    public Task Unsubscribe(Guid userId)
    {

    }

    [HttpGet("")]
    public Task<IActionResult> GetAllParticipants()
    {

    }

    [HttpPut("{id:Guid}")]
    public Task AddParticipant(Guid animalId)
    {

    }

    [HttpDelete("{id:Guid}")]
    public Task DeleteParticipant(Guid animalId)
    {

    }

    [HttpDelete("{id:Guid}")]
    public Task Delete(Guid id)
    {

    }
}
