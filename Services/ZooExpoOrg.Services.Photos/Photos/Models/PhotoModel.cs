using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Photos;

public class PhotoModel
{
    public Guid Id;

    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageMimeType { get; set; }
}

public class PhotoModelProfile : Profile
{
    public PhotoModelProfile()
    {
        CreateMap<PhotoEntity, PhotoModel>()
            .BeforeMap<PhotoModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.LocationId, opt => opt.Ignore());
    }

    public class PhotoModelActions : IMappingAction<PhotoEntity, PhotoModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public PhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(PhotoEntity source, PhotoModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var client = db.Clients.FirstOrDefault(x => x.Id == source.ClientId);
            var animal = db.Animals.FirstOrDefault(x => x.Id == source.AnimalId);
            var exposition = db.Expositions.FirstOrDefault(x => x.Id == source.ExpositionId);


            if (client != null)
            {
                destination.LocationId = client.Uid;
            }
            else if(animal != null)
            {
                destination.LocationId = animal.Uid;
            }
            else if (exposition != null)
            {
                destination.LocationId = exposition.Uid;
            }
            else
            {
                throw new NullReferenceException();
            }

            var owner = db.Clients.FirstOrDefault(x => x.Id == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Uid;
        }
    }
}