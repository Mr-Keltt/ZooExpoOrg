using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Photos;

public class CreatePhotoModel
{
    public Guid OwnerId;
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
}

public class CreatePhotoModelProfile : Profile
{
    public CreatePhotoModelProfile()
    {
        CreateMap<CreatePhotoModel, AnimalPhotoEntity> ()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<CreatePhotoModel, ExpositionPhotoEntity>()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<CreatePhotoModel, ClientPhotoEntity>()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class CreatePhotoModelActions :
        IMappingAction<CreatePhotoModel, AnimalPhotoEntity>,
        IMappingAction<CreatePhotoModel, ExpositionPhotoEntity>,
        IMappingAction<CreatePhotoModel, ClientPhotoEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreatePhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(CreatePhotoModel source, AnimalPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Animals.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.OwnerId = owner.Id;
        }

        public async void Process(CreatePhotoModel source, ExpositionPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.OwnerId = owner.Id;
        }

        public async void Process(CreatePhotoModel source, ClientPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Clients.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.OwnerId = owner.Id;
        }
    }
}
