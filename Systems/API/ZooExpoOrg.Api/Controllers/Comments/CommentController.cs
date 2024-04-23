using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Security;
using ZooExpoOrg.Services.Comments;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.RightVerifier;

namespace ZooExpoOrg.Api.Controllers.Comments;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Technical")]
[Route("v{version:apiVersion}/comment")]
public class CommentController : Controller
{
    private readonly IAppLogger logger;
    private readonly ICommentService commentService;
    private readonly IMapper mapper;
    private readonly IRightVerifierService rightVerifier;

    public CommentController(
        IAppLogger logger, 
        ICommentService commentService, 
        IMapper mapper,
        IRightVerifierService rightVerifier
        )
    {
        this.logger = logger;
        this.commentService = commentService;
        this.mapper = mapper;
        this.rightVerifier = rightVerifier;
    }

    [HttpGet("located/{locationId:Guid}")]
    public async Task<IActionResult> GetLocatedIn(Guid locationId)
    {
        try
        {
            var result = await commentService.GetLocatedIn(locationId);

            if (result == null)
            {
                return NotFound($"Comments not found.");
            }

            return Ok(mapper.Map<IEnumerable<PresintationCommentModel>>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {

        var result = await commentService.GetById(id);

        if (result == null)
        {
            return NotFound($"Comments (ID={id}) not found.");
        }

        return Ok(mapper.Map<PresintationCommentModel>(result));
    }

    [HttpPost]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Create(PresintationCreateCommentModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfCreateComment(jwtToken, model.AuthorId)))
            {
                return BadRequest("Access denied.");
            }

            var result = await commentService.Create(mapper.Map<CreateCommentModel>(model));

            return Ok(mapper.Map<PresintationCommentModel>(result));
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Update(Guid id, PresintationUpdateCommentModel model)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagComment(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await commentService.Update(id, mapper.Map<UpdateCommentModel>(model));

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.UseScope)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!(await rightVerifier.VerifRightsOfManagComment(jwtToken, id)))
            {
                return BadRequest("Access denied.");
            }

            await commentService.Delete(id);

            return Ok();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
    }
}
