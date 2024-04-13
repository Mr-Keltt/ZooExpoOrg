using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ZooExpoOrg.Services.Comments.CommentModelProfile;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Comments;

public class UpdateCommentModel
{
    public string Text { get; set; }
}

public class UpdateCommentModelProfile : Profile
{
    public UpdateCommentModelProfile()
    {
        CreateMap<UpdateCommentModel, CommentEntity>();
    }
}