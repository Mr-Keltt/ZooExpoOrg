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

    public async Task<IEnumerable<PhotoModel>> GetAllOwnedById(Guid OwnerId)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == OwnerId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == OwnerId);
        var client = await db.Clients.FirstOrDefaultAsync(x => x.Uid == OwnerId);

        IEnumerable<PhotoModel> result = null;

        if (animal != null)
        {
            result = mapper.Map<IEnumerable<PhotoModel>>(animal.Photos);
        }
        else if (exposition != null)
        {
            result = mapper.Map<IEnumerable<PhotoModel>>(exposition.Photos);
        }
        else if (client != null)
        {
            var photo = await db.ClientsPhotos.FirstOrDefaultAsync(x => x.Id == client.PhotoId);

            var curList = new List<ClientPhotoEntity>();
            curList.Add(photo);

            result = mapper.Map<IEnumerable<PhotoModel>>(curList);
        }
        else
        {
            throw new ProcessException($"Photo not found.");
        }

        return result;
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animalPhotos = await db.AnimalsPhotos.FirstOrDefaultAsync(x => x.Uid == id);
        var expositionPhotos = await db.ExpositionsPhotos.FirstOrDefaultAsync(x => x.Uid == id);
        var clientPhotos = await db.ClientsPhotos.FirstOrDefaultAsync(x => x.Uid == id);

        if (animalPhotos != null)
            return mapper.Map<PhotoModel>(animalPhotos);
        else if (expositionPhotos != null)
            return mapper.Map<PhotoModel>(expositionPhotos);
        else if (clientPhotos != null)
            return mapper.Map<PhotoModel>(clientPhotos);
        else
            throw new ProcessException($"Photo (ID = {id}) not found.");
    }

    public async Task<PhotoModel> Create(CreatePhotoModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals
            .FirstOrDefaultAsync(x => x.Uid == model.OwnerId);

        var exposition = await context.Expositions
            .FirstOrDefaultAsync(x => x.Uid == model.OwnerId);

        var client = await context.Clients
            .FirstOrDefaultAsync(x => x.Uid == model.OwnerId);

        PhotoModel result = null;

        if (animal != null)
        {
            var photo = mapper.Map<AnimalPhotoEntity>(model);
            await context.AnimalsPhotos.AddAsync(photo);

            animal.Photos.Add(photo);

            result = mapper.Map<PhotoModel>(photo);
        }
        else if (exposition != null)
        {
            var photo = mapper.Map<ExpositionPhotoEntity>(model);
            await context.ExpositionsPhotos.AddAsync(photo);

            exposition.Photos.Add(photo);

            result = mapper.Map<PhotoModel>(photo);
        }
        else if (client != null)
        {
            var photo = mapper.Map<ClientPhotoEntity>(model);
            
            var curPhoto = await context.ClientsPhotos.FirstOrDefaultAsync(x => x.Id == client.PhotoId);

            if (curPhoto != null)
            {
                context.ClientsPhotos.Remove(curPhoto);
            }

            context.ClientsPhotos.Add(photo);

            result = mapper.Map<PhotoModel>(photo);

            await context.SaveChangesAsync();

            var newPhoto = await context.ClientsPhotos.FirstOrDefaultAsync(x => x.Uid == result.Id);

            client.PhotoId = newPhoto.Id;
        }
        else
        {
            throw new ProcessException($"Photo owner (ID = {model.OwnerId}) not found.");
        }

        await context.SaveChangesAsync();
        return result;
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalPhoto = await context.AnimalsPhotos.FirstOrDefaultAsync(x => x.Uid == id);
        var expositionPhoto = await context.ExpositionsPhotos.FirstOrDefaultAsync(x => x.Uid == id);
        var clientPhoto = await context.ClientsPhotos.FirstOrDefaultAsync(x => x.Uid == id);

        if (animalPhoto != null)
        {
            var owner = await context.Animals.FirstOrDefaultAsync(x => x.Id == animalPhoto.OwnerId);

            owner.Photos.Remove(animalPhoto);
            context.AnimalsPhotos.Remove(animalPhoto);
        }
        else if (expositionPhoto != null)
        {
            var owner = await context.Expositions.FirstOrDefaultAsync(x => x.Id == expositionPhoto.OwnerId);

            owner.Photos.Remove(expositionPhoto);
            context.ExpositionsPhotos.Remove(expositionPhoto);
        }
        else if (clientPhoto != null)
        {
            var owner = await context.Clients.FirstOrDefaultAsync(x => x.Id == clientPhoto.OwnerId);

            owner.PhotoId = null;
            context.ClientsPhotos.Remove(clientPhoto);
        }
        else
        {
            throw new ProcessException($"Photo (ID = {id}) not found.");
        }

        await context.SaveChangesAsync();
    }
}
