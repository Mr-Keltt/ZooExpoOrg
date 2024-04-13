using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        var animalComments = await db.AnimalsComments.FirstOrDefaultAsync(x => x.Uid == id);
        var expositionComments = await db.ExpositionsComments.FirstOrDefaultAsync(x => x.Uid == id);

        if (animalComments != null)
        {
            return mapper.Map<CommentModel>(animalComments);
        }
        else if (expositionComments != null)
        {
            return mapper.Map<CommentModel>(expositionComments);
        }
        else
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }
    }

    public async Task<CommentModel> Create(CreateCommentModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == model.LocationId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == model.LocationId);

        if (animal != null)
        {
            var comment = mapper.Map<AnimalCommentEntity>(model);

            db.AnimalsComments.Add(comment);

            animal.Comments.Add(comment);

            db.SaveChanges();

            return mapper.Map<CommentModel>(comment);
        }
        else if (exposition != null)
        {
            var comment = mapper.Map<ExpositionCommentEntity>(model);

            db.ExpositionsComments.Add(comment);

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

        var animalComments = await db.AnimalsComments.FirstOrDefaultAsync(x => x.Uid == id);
        var expositionComments = await db.ExpositionsComments.FirstOrDefaultAsync(x => x.Uid == id);

        if (animalComments != null)
        {
            animalComments = mapper.Map(model, animalComments);

            db.AnimalsComments.Update(animalComments);

            await db.SaveChangesAsync();
        }
        else if (expositionComments != null)
        {
            expositionComments = mapper.Map(model, expositionComments);

            db.ExpositionsComments.Update(expositionComments);

            await db.SaveChangesAsync();
        }
        else
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }
    }

    public async Task Delete(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animalComments = await db.AnimalsComments.FirstOrDefaultAsync(x => x.Uid == id);
        var expositionComments = await db.ExpositionsComments.FirstOrDefaultAsync(x => x.Uid == id);

        if (animalComments != null)
        {
            db.AnimalsComments.Remove(animalComments);

            await db.SaveChangesAsync();
        }
        else if (expositionComments != null)
        {
            db.ExpositionsComments.Remove(expositionComments);

            await db.SaveChangesAsync();
        }
        else
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }
    }
}