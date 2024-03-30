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
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<ExpositionPhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

        CreateMap<UserPhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class PhotoModelActions : 
        IMappingAction<AnimalPhotoEntity, PhotoModel>,
        IMappingAction<ExpositionPhotoEntity, PhotoModel>,
        IMappingAction<UserPhotoEntity, PhotoModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public PhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(AnimalPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var photo = await db.AnimalsPhotos
                .Include(x => x.Animal)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = photo.Uid;
            destination.OwnerId = photo.Animal.Uid;
        }

        public async void Process(ExpositionPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var photo = await db.ExpositionsPhotos
                .Include(x => x.Exposition)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = photo.Uid;
            destination.OwnerId = photo.Exposition.Uid;
        }

        public async void Process(UserPhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var photo = await db.UsersPhotos
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = photo.Uid;
            destination.OwnerId = photo.User.Uid;
        }
    }
}