using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;
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
}

public class ClientModelProfile : Profile
{
    public ClientModelProfile()
    {
        CreateMap<ClientEntity, ClientModel>()
            .BeforeMap<ClientModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Patronymic, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoId, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
            .ForMember(dest => dest.OrganizedExpositions, opt => opt.Ignore())
            .ForMember(dest => dest.Animals, opt => opt.Ignore());
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
            using var db = contextFactory.CreateDbContext();

            var client = await db.Clients
                .Include(x => x.Photo)
                .Include(x => x.Subscriptions)
                .Include(x => x.OrganizedExpositions)
                .Include(x => x.Animals)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            var photo = await db.ClientsPhotos
                .FirstOrDefaultAsync(x => x.Id == client.PhotoId);

            var subscriptionsIds = client.Subscriptions
                .Select(sub => sub.Uid)
                .ToList();

            var organizedExpositionsIds = client.OrganizedExpositions
                .Select(sub => sub.Uid)
                .ToList();

            var animalsIds = client.Animals
                .Select(sub => sub.Uid)
                .ToList();

            destination.Id = client.Uid;
            destination.Patronymic = !client.Patronymic.IsNullOrEmpty() ? client.Patronymic : "";
            destination.PhotoId = !(client.PhotoId == null) ? photo.Uid : null;
            destination.Subscriptions = subscriptionsIds;
            destination.OrganizedExpositions = organizedExpositionsIds;
            destination.Animals = animalsIds;
        }
    }
}