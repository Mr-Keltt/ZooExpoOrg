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

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid PhotoId { get; set; }

    //public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    //public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalModel> Animals { get; set; }

    //public virtual ICollection<Comment> Comments { get; set; }
}

public class ClientModelProfile : Profile
{
    public ClientModelProfile()
    {
        CreateMap<ClientEntity, ClientModel>()
            .BeforeMap<ClientModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Patronymic, opt => opt.Ignore());
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
                .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
                .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
                .Include(x => x.Comments)
                .Include(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = client.Uid;
            destination.Patronymic = !client.Patronymic.IsNullOrEmpty() ? client.Patronymic : "";
        }
    }
}