using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Helpers;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Expositions;

public class UpdateExpositionModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}

public class UpdateExpositionModelProfile : Profile
{
    public UpdateExpositionModelProfile()
    {
        CreateMap<UpdateExpositionModel, ExpositionEntity>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart))
            .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd))
            .ForMember(dest => dest.DateStart, opt => opt.Ignore())
            .ForMember(dest => dest.DateEnd, opt => opt.Ignore());
    }

    public class UpdateExpositionModelActions : IMappingAction<UpdateExpositionModel, ExpositionEntity>
    {
        public UpdateExpositionModelActions()
        {

        }

        public void Process(UpdateExpositionModel source, ExpositionEntity destination, ResolutionContext context)
        {
            destination.DateStart = DateHelper.ConvertToUTC(source.DateStart);
            destination.DateEnd = DateHelper.ConvertToUTC(source.DateEnd);
        }
    }
}

public class UpdateExpositionModelValidator : AbstractValidator<UpdateExpositionModel>
{
    public UpdateExpositionModelValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(10000).WithMessage("Text must be less than 10000 characters.");
        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.")
            .NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.")
            .NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Street)
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters.")
            .NotEmpty().WithMessage("Street is required.");
        RuleFor(x => x.HouseNumber)
            .MaximumLength(100).WithMessage("HouseNumber must not exceed 50 characters.");
        RuleFor(x => x.DateStart)
            .NotEmpty().WithMessage("DateStart is required.")
            .Must(BeAValidDate).WithMessage("DateStart must be a valid date.");
        RuleFor(x => x.DateEnd)
            .NotEmpty().WithMessage("DateEnd is required.")
            .Must(BeAValidDate).WithMessage("DateEnd must be a valid date.")
            .GreaterThanOrEqualTo(x => x.DateStart).WithMessage("DateEnd must be greater than or equal to DateStart.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date < DateTime.Now && date > DateTime.Now.AddYears(-150);
    }
}