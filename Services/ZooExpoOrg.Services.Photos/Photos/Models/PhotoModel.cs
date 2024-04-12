using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Photos;

public class PhotoModel
{
    public Guid Id;
    public Guid OwnerId;
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
}

public class PhotoModelProfile : Profile
{
    public PhotoModelProfile()
    {
        CreateMap<AnimalPhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<ExpositionPhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<ClientPhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class PhotoModelActions : 
        IMappingAction<AnimalPhotoEntity, PhotoModel>,
        IMappingAction<ExpositionPhotoEntity, PhotoModel>,
        IMappingAction<ClientPhotoEntity, PhotoModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public PhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AnimalPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = db.Animals.FirstOrDefault(x => x.Id == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Uid;

            db.Dispose();
        }

        public async void Process(ExpositionPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = db.Expositions.FirstOrDefault(x => x.Id == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Uid;

            db.Dispose();
        }

        public async void Process(ClientPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = db.Clients.FirstOrDefault(x => x.Id == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Uid;

            db.Dispose();
        }
    }
}