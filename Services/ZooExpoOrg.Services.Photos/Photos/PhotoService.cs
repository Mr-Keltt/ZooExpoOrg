using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public PhotoService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper,
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<PhotoModel>> GetAllLocationById(Guid OwnerId)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == OwnerId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == OwnerId);
        var client = await db.Clients.FirstOrDefaultAsync(x => x.Uid == OwnerId);

        ICollection<PhotoEntity> photos = null;

        if (animal != null)
        {
            photos = animal.Photos;
        }
        else if (exposition != null)
        {
            photos = exposition.Photos;
        }
        else if (client != null)
        {
            photos = client.Photos;
        }
        else
        {
            throw new ProcessException($"Photo not found.");
        }

        return mapper.Map<IEnumerable<PhotoModel>>(photos);
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photo = await context.Photos.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<PhotoModel>(photo);
    }

    public async Task<PhotoModel> Create(CreatePhotoModel model)
    {
        using var db = dbContextFactory.CreateDbContext();

        var owner = db.Clients.FirstOrDefault(x => x.Uid == model.OwnerId);

        if (owner == null) 
        {
            throw new ProcessException($"User (ID = {model.OwnerId}) not found.");
        }

        var client = db.Clients.FirstOrDefault(x => x.Uid == model.LocationId);
        var animal = db.Animals.FirstOrDefault(x => x.Uid == model.LocationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == model.LocationId);

        ICollection<PhotoEntity> location = null;

        if (client != null)
        {
            location = client.Photos;
        }
        else if (animal != null)
        {
            location = animal.Photos;
        }
        else if (exposition != null)
        {
            location = exposition.Photos;
        }
        else
        {
            throw new ProcessException($"Location (ID = {model.LocationId}) not found.");
        }

        var photo = mapper.Map<PhotoEntity>(model);

        db.Photos.Add(photo);

        owner.Photos.Add(photo);

        location.Add(photo);

        db.SaveChanges();

        return mapper.Map<PhotoModel>(photo);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photo = await context.Photos.FirstOrDefaultAsync(x => x.Uid == id);
        
        if (photo == null)
        {
            throw new ProcessException($"Photo (ID = {id}) not found.");
        }

        var owner = await context.Clients.FirstOrDefaultAsync(x => x.Id == photo.OwnerId);
        var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == photo.ClientId);
        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id == photo.AnimalId);
        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Id == photo.ExpositionId);

        if (client != null)
        {
            client.Photos.Remove(photo);
        }
        else if (animal != null)
        {
            animal.Photos.Remove(photo);
        }
        else if (exposition != null)
        {
            exposition.Photos.Add(photo);
        }

        context.Photos.Remove(photo);

        owner.Photos.Remove(photo);

        await context.SaveChangesAsync();
    }
}
