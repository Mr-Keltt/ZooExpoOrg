using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;
using static ZooExpoOrg.Services.Clients.ClientModelProfile;

namespace ZooExpoOrg.Services.Clients; 

public class UpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid? PhotoId { get; set; }

    public virtual ICollection<Guid> Subscriptions { get; set; }

    public virtual ICollection<Guid> OrganizedExpositions { get; set; }

    public virtual ICollection<Guid> Animals { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<UpdateClientModel, ClientEntity>()
            .BeforeMap<UpdateClientModelActions>()
            .ForMember(dest => dest.PhotoId, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
            .ForMember(dest => dest.OrganizedExpositions, opt => opt.Ignore())
            .ForMember(dest => dest.Animals, opt => opt.Ignore());
    }

    public class UpdateClientModelActions : IMappingAction<UpdateClientModel, ClientEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateClientModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(UpdateClientModel source, ClientEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var photo = await db.ClientsPhotos
                .FirstOrDefaultAsync(x => x.Uid == source.PhotoId);

            var subscriptions = await db.Expositions
                .Where(e => source.Subscriptions.Contains(e.Uid))
                .ToListAsync();

            var organizedExpositions = await db.Expositions
                .Where(e => source.OrganizedExpositions.Contains(e.Uid))
                .ToListAsync();

            var animals = await db.Animals
                .Where(e => source.Animals.Contains(e.Uid))
                .ToListAsync();

            destination.PhotoId = !(source.PhotoId == null) ? photo.Id : null;
            destination.Subscriptions = subscriptions;
            destination.OrganizedExpositions = organizedExpositions;
            destination.Animals = animals;
        }
    }
}