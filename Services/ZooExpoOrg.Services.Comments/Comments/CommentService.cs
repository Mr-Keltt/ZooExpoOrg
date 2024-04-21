using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IModelValidator<CreateCommentModel> createCommentModelValidator;
    private readonly IModelValidator<UpdateCommentModel> updateCommentModelValidator;

    public CommentService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAppLogger logger,
        IModelValidator<CreateCommentModel> createCommentModelValidator,
        IModelValidator<UpdateCommentModel> updateCommentModelValidator
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
        this.createCommentModelValidator = createCommentModelValidator;
        this.updateCommentModelValidator = updateCommentModelValidator;
    }

    public async Task<IEnumerable<CommentModel>> GetLocatedIn(Guid locationId)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == locationId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == locationId);

        if (animal != null)
        {
            return mapper.Map<IEnumerable<CommentModel>>(animal.Comments);
        }
        else if (exposition != null)
        {
            return mapper.Map<IEnumerable<CommentModel>>(exposition.Comments);
        }
        else
        {
            throw new ProcessException($"Location (ID = {locationId}) not found.");
        }
    }

    public async Task<CommentModel> GetById(Guid id)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var comments = await db.Comments.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<CommentModel>(comments);
    }

    public async Task<CommentModel> Create(CreateCommentModel model)
    {
        createCommentModelValidator.Check(model);

        using var db = dbContextFactory.CreateDbContext();

        var author = db.Clients.FirstOrDefault(x => x.Uid == model.AuthorId);

        if (author == null)
        {
            throw new ProcessException($"Client (ID = {model.AuthorId}) not found.");
        }

        var animal = db.Animals.FirstOrDefault(x => x.Uid == model.LocationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == model.LocationId);

        ICollection<CommentEntity> location = null; 

        if (animal != null)
        {
            location = animal.Comments;
        }
        else if (exposition != null)
        {
            location = exposition.Comments;
        }
        else
        {
            throw new ProcessException($"Location (ID = {model.LocationId}) not found.");
        }

        var comment = mapper.Map<CommentEntity>(model);

        db.Comments.Add(comment);

        location.Add(comment);

        author.Comments.Add(comment);

        db.SaveChanges();

        return mapper.Map<CommentModel>(comment);
    }

    public async Task Update(Guid id, UpdateCommentModel model)
    {
        updateCommentModelValidator.Check(model);

        using var db = await dbContextFactory.CreateDbContextAsync();

        var comments = await db.Comments.FirstOrDefaultAsync(x => x.Uid == id);

        if (comments == null)
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }

        comments = mapper.Map(model, comments);

        db.Comments.Update(comments);

        await db.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var comment = await db.Comments.FirstOrDefaultAsync(x => x.Uid == id);

        if (comment == null)
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }

        var author = await db.Clients.FirstOrDefaultAsync(x => x.Id == comment.AuthorId);
        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Id == comment.AnimalId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Id == comment.ExpositionId);

        if (animal != null)
        {
            animal.Comments.Remove(comment);
        }
        else
        {
            exposition.Comments.Remove(comment);
        }

        db.Comments.Remove(comment);

        author.Comments.Remove(comment);

        await db.SaveChangesAsync();
    }
}