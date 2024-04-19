using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZooExpoOrg.Services.Comments;

namespace ZooExpoOrg.Api.Controllers;

public class PresintationUpdateCommentModel
{
    public string Text { get; set; }
}

public class PresintationUpdateCommentModelProfile : Profile
{
    public PresintationUpdateCommentModelProfile()
    {
        CreateMap<PresintationUpdateCommentModel, UpdateCommentModel>();
    }
}
