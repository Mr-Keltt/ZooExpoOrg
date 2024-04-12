using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Clients; 

public class UpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid? PhotoId { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<UpdateClientModel, ClientEntity>()
            .BeforeMap<UpdateClientModelActions>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(dest => dest.PhotoId, opt => opt.Ignore());
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
            using var db = await contextFactory.CreateDbContextAsync();

            var photo = await db.ClientsPhotos.FirstOrDefaultAsync(x => x.Uid == source.PhotoId);

            destination.PhotoId = photo != null ? photo.Id : null;
        }
    }
}