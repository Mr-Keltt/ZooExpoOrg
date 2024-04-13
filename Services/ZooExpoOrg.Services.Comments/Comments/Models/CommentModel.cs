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
        CreateMap<CommentEntity, CommentModel>()
            .BeforeMap<CommentModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.LocationId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }

    public class CommentModelActions : IMappingAction<CommentEntity, CommentModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CommentModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CommentEntity source, CommentModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var client = db.Clients.FirstOrDefault(x => x.Id == source.AuthorId);

            if (client == null)
            {
                throw new NullReferenceException();
            }

            var animal = db.Animals.FirstOrDefault(x => x.Id == source.AnimalId);
            var exposition = db.Expositions.FirstOrDefault(x => x.Id == source.ExpositionId);

            if (animal != null)
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

            destination.AuthorId = client.Uid;
        }
    }
}