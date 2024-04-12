using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;
using ZooExpoOrg.Context.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;

namespace ZooExpoOrg.Services.Clients;

public class ClientModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid? PhotoId { get; set; }

    public virtual ICollection<Guid> Subscriptions { get; set; }

    public virtual ICollection<Guid> OrganizedExpositions { get; set; }

    public virtual ICollection<Guid> Animals { get; set; }

    public virtual ICollection<Guid> Comments { get; set; }
}

public class ClientModelProfile : Profile
{
    public ClientModelProfile()
    {
        CreateMap<ClientEntity, ClientModel>()
            .BeforeMap<ClientModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions.Select(e => e.Uid)))
            .ForMember(dest => dest.OrganizedExpositions, opt => opt.MapFrom(src => src.OrganizedExpositions.Select(e => e.Uid)))
            .ForMember(dest => dest.Animals, opt => opt.MapFrom(src => src.Animals.Select(e => e.Uid)))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(e => e.Uid)))
            .ForMember(dest => dest.PhotoId, opt => opt.Ignore());
    }

    public class ClientModelActions : IMappingAction<ClientEntity, ClientModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public ClientModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(ClientEntity source, ClientModel destination, ResolutionContext context)
        {
            using var db = await contextFactory.CreateDbContextAsync();

            var photo = await db.ClientsPhotos.FirstOrDefaultAsync(x => x.Id == source.PhotoId);

            destination.PhotoId = photo != null ? photo.Uid : null;
        }
    }
}