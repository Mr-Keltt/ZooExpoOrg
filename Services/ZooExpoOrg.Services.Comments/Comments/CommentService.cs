using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public CommentService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<CommentModel>> GetLocatedIn(Guid locationId)
    {
        using var db = dbContextFactory.CreateDbContext();

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
        using var db = dbContextFactory.CreateDbContext();

        var comments = await db.Comments.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<CommentModel>(comments);
    }

    public async Task<CommentModel> Create(CreateCommentModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animal = db.Animals.FirstOrDefault(x => x.Uid == model.LocationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == model.LocationId);

        if (animal != null)
        {
            var comment = mapper.Map<CommentEntity>(model);

            db.Comments.Add(comment);

            animal.Comments.Add(comment);

            db.SaveChanges();

            return mapper.Map<CommentModel>(comment);
        }
        else if (exposition != null)
        {
            var comment = mapper.Map<CommentEntity>(model);

            db.Comments.Add(comment);

            exposition.Comments.Add(comment);

            db.SaveChanges();

            return mapper.Map<CommentModel>(comment);
        }
        else
        {
            throw new ProcessException($"Location (ID = {model.LocationId}) not found.");
        }
    }

    public async Task Update(Guid id, UpdateCommentModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

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
        using var db = dbContextFactory.CreateDbContext();

        var comments = await db.Comments.FirstOrDefaultAsync(x => x.Uid == id);

        if (comments == null)
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }

        db.Comments.Remove(comments);

        await db.SaveChangesAsync();
    }
}