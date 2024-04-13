using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Comments;

public class CreateCommentModel
{
    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}

public class CreateCommentModelProfile : Profile
{
    public CreateCommentModelProfile()
    {
        CreateMap<CreateCommentModel, AnimalCommentEntity> ()
            .BeforeMap<CreateCommentModelActions>()
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());

        CreateMap<CreateCommentModel, ExpositionCommentEntity>()
            .BeforeMap<CreateCommentModelActions>()
            .ForMember(dest => dest.ExpositionId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }

    public class CreateCommentModelActions :
        IMappingAction<CreateCommentModel, AnimalCommentEntity>,
        IMappingAction<CreateCommentModel, ExpositionCommentEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateCommentModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateCommentModel source, AnimalCommentEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animal = db.Animals.FirstOrDefault(db => db.Uid == source.LocationId);

            if (animal == null)
            {
                throw new NullReferenceException();
            }

            var client = db.Clients.FirstOrDefault(x => x.Uid == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            destination.AnimalId = animal.Id;
            destination.AuthorId = client.Id;
        }

        public void Process(CreateCommentModel source, ExpositionCommentEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var exposition = db.Expositions.FirstOrDefault(db => db.Uid == source.LocationId);

            if (exposition == null)
            {
                throw new NullReferenceException();
            }

            var client = db.Clients.FirstOrDefault(x => x.Uid == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            destination.ExpositionId = exposition.Id;
            destination.AuthorId = client.Id;
        }
    }
}