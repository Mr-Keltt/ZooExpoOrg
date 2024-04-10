using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
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
            .ForMember(dest => dest.PhotoId, opt => opt.Ignore())
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

        public async void Process(CreateClientModel source, ClientEntity destination, ResolutionContext context)
        {
            destination.PhotoId = null;
            destination.Subscriptions = new List<ExpositionEntity>();
            destination.OrganizedExpositions = new List<ExpositionEntity>();
            destination.Animals = new List<AnimalEntity>();
            destination.Comments = new List<CommentEntity>();
        }
    }
}