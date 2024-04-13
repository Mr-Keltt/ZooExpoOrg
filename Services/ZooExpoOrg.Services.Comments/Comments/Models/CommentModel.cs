using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Comments;

public class CommentModel
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<AnimalCommentEntity, CommentModel>()
            .BeforeMap<CommentModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.LocationId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());

        CreateMap<ExpositionCommentEntity, CommentModel>()
            .BeforeMap<CommentModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.LocationId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }

    public class CommentModelActions : 
        IMappingAction<AnimalCommentEntity, CommentModel>,
        IMappingAction<ExpositionCommentEntity, CommentModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CommentModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(AnimalCommentEntity source, CommentModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animal = db.Animals.FirstOrDefault(db => db.Id == source.AnimalId);

            if (animal == null)
            {
                throw new NullReferenceException();
            }

            var client = db.Clients.FirstOrDefault(x => x.Id == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            destination.LocationId = animal.Uid;
            destination.AuthorId = client.Uid;
        }

        public void Process(ExpositionCommentEntity source, CommentModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var exposition = db.Expositions.FirstOrDefault(db => db.Id == source.ExpositionId);

            if (exposition == null)
            {
                throw new NullReferenceException();
            }

            var client = db.Clients.FirstOrDefault(x => x.Id == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            destination.LocationId = exposition.Uid;
            destination.AuthorId = client.Uid;
        }
    }
}