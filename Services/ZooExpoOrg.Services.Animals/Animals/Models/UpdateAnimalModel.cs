using AutoMapper;
using FluentValidation;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Animals.Animals;

public class UpdateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }
}

public class UpdateAnimalModelProfile : Profile
{
    public UpdateAnimalModelProfile()
    {
        CreateMap<UpdateAnimalModel, AnimalEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight));
    }
}

public class UpdateAnimalModelValidator : AbstractValidator<UpdateAnimalModel>
{
    public UpdateAnimalModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .Must(BeAValidDate).WithMessage("BirthDate must be a valid date.");

        RuleFor(x => x.Height)
            .NotEmpty().WithMessage("Height is required.")
            .GreaterThan(0).WithMessage("Height must be greater than 0.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required.")
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}