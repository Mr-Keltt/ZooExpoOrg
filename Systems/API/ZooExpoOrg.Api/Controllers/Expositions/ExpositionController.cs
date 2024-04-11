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
        throw new NotImplementedException();
    }

    [HttpGet("{id:Guid}")]
    public Task<IActionResult> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("")]
    public Task<PresintationExpositionModel> Create(CreateExpositionModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:Guid}")]
    public Task Update(Guid id, UpdateExpositionModel model)
    {
        throw new NotImplementedException();
    }

    [HttpGet("")]
    public Task<IActionResult> GetAllSubscribers()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:Guid}")]
    public Task Subscribe(Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:Guid}")]
    public Task Unsubscribe(Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("")]
    public Task<IActionResult> GetAllParticipants()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:Guid}")]
    public Task AddParticipant(Guid animalId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:Guid}")]
    public Task DeleteParticipant(Guid animalId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:Guid}")]
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
