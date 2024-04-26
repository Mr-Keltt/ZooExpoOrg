using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using FluentValidation;
using ZooExpoOrg.Common.Helpers;

namespace ZooExpoOrg.Services.Animals.Achievements;

public class CreateAchievementModel
{
    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}

public class CreateAchievementModelProfile : Profile
{
    public CreateAchievementModelProfile()
    {
        CreateMap<CreateAchievementModel, AchievementEntity>()
            .BeforeMap<CreateAchievementModelActions>()
            .ForMember(dest => dest.DateAward, opt => opt.Ignore())
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore());
    }

    public class CreateAchievementModelActions : IMappingAction<CreateAchievementModel, AchievementEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAchievementModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateAchievementModel source, AchievementEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animal = db.Animals.FirstOrDefault(x => x.Uid == source.AnimalId);

            if (animal == null)
            {
                throw new NullReferenceException(); 
            }

            destination.DateAward = DateHelper.ConvertToUTC(source.DateAward);
            destination.AnimalId = animal.Id;
        }
    }
}

public class CreateAchievementModelValidator : AbstractValidator<CreateAchievementModel>
{
    public CreateAchievementModelValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("AnimalId is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(10000).WithMessage("Description cannot be longer than 10000 characters.");
        RuleFor(x => x.DateAward)
            .NotEmpty().WithMessage("DateAward is required.")
            .Must(BeAValidDate).WithMessage("DateAward must be a valid date.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date < DateTime.Now && date > DateTime.Now.AddYears(-150);
    }
}