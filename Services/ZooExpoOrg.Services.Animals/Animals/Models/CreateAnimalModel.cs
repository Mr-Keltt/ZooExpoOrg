using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using Newtonsoft.Json.Linq;
using System.Data;
using FluentValidation;
using ZooExpoOrg.Services.Animals.Achievements;

namespace ZooExpoOrg.Services.Animals.Animals;

public class CreateAnimalModel
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AnimalType Type { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }
}

public class CreateAnimalModelProfile : Profile
{
    public CreateAnimalModelProfile()
    {
        CreateMap<CreateAnimalModel, AnimalEntity>()
            .BeforeMap<CreateAnimalModelActions>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class CreateAnimalModelActions : IMappingAction<CreateAnimalModel, AnimalEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAnimalModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateAnimalModel source, AnimalEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = db.Clients.FirstOrDefault(x => x.Uid == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Id;     
            destination.Comments = new List<CommentEntity>();
            destination.Photos = new List<PhotoEntity>();
            destination.Achievements = new List<AchievementEntity>();
        }
    }
}

public class CreateAnimalModelValidator : AbstractValidator<CreateAnimalModel>
{
    public CreateAnimalModelValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(10000).WithMessage("Description must not exceed 10000 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .Must(BeAValidDate).WithMessage("BirthDate must be a valid date.");

        RuleFor(x => x.Height)
            .NotEmpty().WithMessage("Height is required.")
            .GreaterThan(-1).WithMessage("Height must be greater than -1.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required.")
            .GreaterThan(-1).WithMessage("Weight must be greater than -1.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}