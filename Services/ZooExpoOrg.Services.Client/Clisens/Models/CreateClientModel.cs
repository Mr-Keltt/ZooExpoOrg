using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Common.Helpers;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Clients;

public class CreateClientModel
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }
}

public class CreateClientModelProfile : Profile
{
    public CreateClientModelProfile()
    {
        CreateMap<CreateClientModel, ClientEntity>()
            .BeforeMap<CreateClientModelActions>()
            .ForMember(dest => dest.BirthDate, opt => opt.Ignore())
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ForMember(dest => dest.OwnedPhotos, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
            .ForMember(dest => dest.OrganizedExpositions, opt => opt.Ignore())
            .ForMember(dest => dest.Animals, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
    }

    public class CreateClientModelActions : IMappingAction<CreateClientModel, ClientEntity>
    {
        public CreateClientModelActions()
        {
            
        }

        public void Process(CreateClientModel source, ClientEntity destination, ResolutionContext context)
        {
            destination.BirthDate = DateHelper.ConvertToUTC(source.BirthDate);
            destination.Photos = new List<PhotoEntity>();
            destination.OwnedPhotos = new List<PhotoEntity>();
            destination.Subscriptions = new List<ExpositionEntity>();
            destination.OrganizedExpositions = new List<ExpositionEntity>();
            destination.Animals = new List<AnimalEntity>();
            destination.Comments = new List<CommentEntity>();
        }
    }
}

public class CreateClientModelValidator : AbstractValidator<CreateClientModel>
{
    public CreateClientModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname must not exceed 50 characters.");
        RuleFor(x => x.Patronymic)
            .MaximumLength(50).WithMessage("Patronymic must not exceed 50 characters.");
        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.");
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .Must(BeAValidDate).WithMessage("Invalid date format.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date < DateTime.Now && date > DateTime.Now.AddYears(-150);
    }
}