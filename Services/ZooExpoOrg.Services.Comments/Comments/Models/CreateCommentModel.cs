using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using FluentValidation;

namespace ZooExpoOrg.Services.Comments;

public class CreateCommentModel
{
    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}

public class CreateCommentModelProfile : Profile
{
    public CreateCommentModelProfile()
    {
        CreateMap<CreateCommentModel, CommentEntity> ()
            .BeforeMap<CreateCommentModelActions>()
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore())
            .ForMember(dest => dest.ExpositionId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }

    public class CreateCommentModelActions : IMappingAction<CreateCommentModel, CommentEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateCommentModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateCommentModel source, CommentEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var client = db.Clients.FirstOrDefault(x => x.Uid == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            var animal = db.Animals.FirstOrDefault(x => x.Uid == source.LocationId);
            var exposition = db.Expositions.FirstOrDefault(x => x.Uid == source.LocationId);

            if (animal != null)
            {
                destination.AnimalId = animal.Id;
                destination.ExpositionId = null;
            }
            else if (exposition != null)
            {
                destination.ExpositionId = exposition.Id;
                destination.AnimalId = null;
            }
            else
            {
                throw new NullReferenceException();
            }

            destination.AuthorId = client.Id;
        }
    }
}

public class CreateCommentModelValidator : AbstractValidator<CreateCommentModel>
{
    public CreateCommentModelValidator()
    {
        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("LocationId is required.");
        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.");
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(10000).WithMessage("Text must be less than 10000 characters.");
        RuleFor(x => x.DateWriting)
            .NotEmpty().WithMessage("DateWriting is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DateWriting must be in the past.");
    }
}
