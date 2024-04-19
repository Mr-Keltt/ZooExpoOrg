using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Comments;

namespace ZooExpoOrg.Api.Controllers;

public class PresintationCreateCommentModel
{
    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}

public class PresintationCreateCommentModelProfile : Profile
{
    public PresintationCreateCommentModelProfile()
    {
        CreateMap<PresintationCreateCommentModel, CreateCommentModel>();
    }
}
