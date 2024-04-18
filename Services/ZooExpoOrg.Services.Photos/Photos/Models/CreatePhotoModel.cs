using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Photos;

public class CreatePhotoModel
{
    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageMimeType { get; set; }
}

public class CreatePhotoModelProfile : Profile
{
    public CreatePhotoModelProfile()
    {
        CreateMap<CreatePhotoModel, PhotoEntity> ()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.ClientId, opt => opt.Ignore())
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore())
            .ForMember(dest => dest.ExpositionId, opt => opt.Ignore());
    }

    public class CreatePhotoModelActions : IMappingAction<CreatePhotoModel, PhotoEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreatePhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreatePhotoModel source, PhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var client = db.Clients.FirstOrDefault(x => x.Uid == source.LocationId);
            var animal = db.Animals.FirstOrDefault(x => x.Uid == source.LocationId);
            var exposition = db.Expositions.FirstOrDefault(x => x.Uid == source.LocationId);

            if (client != null)
            {
                destination.ClientId = client.Id;
                destination.ExpositionId = null;
            }
            else if (animal != null)
            {
                destination.AnimalId = animal.Id;
                destination.ExpositionId = null;
            }
            else if (exposition != null)
            {
                destination.ExpositionId = exposition.Id;
                destination.AnimalId = null;
            }
            else
            {
                throw new NullReferenceException();
            }

            var owner = db.Clients.FirstOrDefault(x => x.Uid == source.OwnerId);

            if (owner == null)
            {
                throw new NullReferenceException();
            }

            destination.OwnerId = owner.Id;
        }
    }
}
