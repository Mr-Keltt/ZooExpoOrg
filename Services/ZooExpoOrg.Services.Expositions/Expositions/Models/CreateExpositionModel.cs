using AutoMapper;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Expositions;

public class CreateExpositionModel
{
    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}

public class CreateExpositionModelProfile : Profile
{
    protected CreateExpositionModelProfile()
    {

        CreateMap<CreateExpositionModel, ExpositionEntity>()
            .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.OrganizerId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart))
            .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd))
            .ForMember(dest => dest.Participants, opt => opt.Ignore())
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Subscribers, opt => opt.Ignore());

    }

    public class CreateExpositionModelAction : IMappingAction<CreateExpositionModel, ExpositionEntity>
    {
        public CreateExpositionModelAction()
        {
        }

        public async void Process(CreateExpositionModel source, ExpositionEntity destination, ResolutionContext context)
        {
            destination.Participants = new List<AnimalEntity>();
            destination.Photos = new List<ExpositionPhotoEntity>();
            destination.Comments = new List<ClientEntity>();
            destination.Subscribers = new List<ClientEntity>();
        }
    }
}