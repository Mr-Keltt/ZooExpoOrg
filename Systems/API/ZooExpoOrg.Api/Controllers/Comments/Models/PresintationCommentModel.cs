using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Comments;

namespace ZooExpoOrg.Api.Controllers;

public class PresintationCommentModel : Controller
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}

public class PresintationCommentModelProfile : Profile
{
    public PresintationCommentModelProfile()
    {
        CreateMap<CommentModel, PresintationCommentModel>();
    }
}
