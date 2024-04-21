using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ZooExpoOrg.Services.Comments.CommentModelProfile;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using FluentValidation;

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

public class UpdateCommentModelValidator : AbstractValidator<UpdateCommentModel>
{
    public UpdateCommentModelValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(10000).WithMessage("Text must be less than 10000 characters.");
    }
}