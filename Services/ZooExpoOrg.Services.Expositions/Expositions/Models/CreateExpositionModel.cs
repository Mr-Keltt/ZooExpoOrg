using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Common.Helpers;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Expositions;

public class CreateExpositionModel
{
    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public AnimalType ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}

public class CreateExpositionModelProfile : Profile
{
    public CreateExpositionModelProfile()
    {
        CreateMap<CreateExpositionModel, ExpositionEntity>()
            .BeforeMap<CreateExpositionModelActions>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ParticipantsType, opt => opt.MapFrom(src => src.ParticipantsType))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart))
            .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd))
            .ForMember(dest => dest.OrganizerId, opt => opt.Ignore())
            .ForMember(dest => dest.Participants, opt => opt.Ignore())
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Subscribers, opt => opt.Ignore())
            .ForMember(dest => dest.DateStart, opt => opt.Ignore())
            .ForMember(dest => dest.DateEnd, opt => opt.Ignore());
    }

    public class CreateExpositionModelActions : IMappingAction<CreateExpositionModel, ExpositionEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateExpositionModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateExpositionModel source, ExpositionEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var organizer =  db.Clients.FirstOrDefault(x => x.Uid == source.OrganizerId);
            
            if (organizer == null)
            {
                throw new NullReferenceException();
            }

            destination.OrganizerId = organizer.Id;
            destination.Participants = new List<AnimalEntity>();
            destination.Photos = new List<PhotoEntity>();
            destination.Comments = new List<CommentEntity>();
            destination.Subscribers = new List<ClientEntity>();
            destination.DateStart = DateHelper.ConvertToUTC(source.DateStart);
            destination.DateEnd = DateHelper.ConvertToUTC(source.DateEnd);
        }
    }
}

public class CreateExpositionModelValidator : AbstractValidator<CreateExpositionModel>
{
    public CreateExpositionModelValidator()
    {
        RuleFor(x => x.OrganizerId)
            .NotEmpty().WithMessage("OrganizerId is required.");
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(10000).WithMessage("Description must be less than 10000 characters.");
        RuleFor(x => x.ParticipantsType)
            .IsInEnum().WithMessage("Invalid ParticipantsType value.");
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