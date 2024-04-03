using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities.Common;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public PhotoService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<PhotoModel>> GetAllOwnedById(Guid OwnerId)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animalPhotos = await db.AnimalsPhotos
                                .Include(x => x.Owner)
                                .Where(x => x.Owner.Uid == OwnerId)
                                .ToListAsync();

        var expositionPhotos = await db.ExpositionsPhotos
                                    .Include(x => x.Owner)
                                    .Where(x => x.Owner.Uid == OwnerId)
                                    .ToListAsync();

        var clientPhotos = await db.ClientsPhotos
                            .Include(x => x.Owner)
                            .Where(x => x.Owner.Uid == OwnerId)
                            .ToListAsync();

        var result = mapper.Map<IEnumerable<PhotoModel>>(animalPhotos)
            .Concat(mapper.Map<IEnumerable<PhotoModel>>(expositionPhotos))
            .Concat(mapper.Map<IEnumerable<PhotoModel>>(clientPhotos));

        return result;
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animalPhotos = await db.AnimalsPhotos
                                .Include(x => x.Owner)
                                .FirstOrDefaultAsync(x => x.Uid == id);

        var expositionPhotos = await db.ExpositionsPhotos
                                    .Include(x => x.Owner)
                                    .FirstOrDefaultAsync(x => x.Uid == id);

        var clientPhotos = await db.ClientsPhotos
                            .Include(x => x.Owner)
                            .FirstOrDefaultAsync(x => x.Uid == id);

        if (animalPhotos != null)
            return mapper.Map<PhotoModel>(animalPhotos);
        else if (expositionPhotos != null)
            return mapper.Map<PhotoModel>(expositionPhotos);
        else if (clientPhotos != null)
            return mapper.Map<PhotoModel>(clientPhotos);
        else
            return null;
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

            result = mapper.Map<PhotoModel>(photo);
        }
        else if (exposition != null)
        {
            var photo = mapper.Map<ExpositionPhotoEntity>(model);
            await context.ExpositionsPhotos.AddAsync(photo);

            result = mapper.Map<PhotoModel>(photo);
        }
        else if (client != null)
        {
            var photo = mapper.Map<ClientPhotoEntity>(model);
            await context.ClientsPhotos.AddAsync(photo);

            result = mapper.Map<PhotoModel>(photo);
        }
        else
        {
            return null;
        }

        await context.SaveChangesAsync();
        return result;
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.AnimalsPhotos
            .FirstOrDefaultAsync(x => x.Uid == id);

        var exposition = await context.ExpositionsPhotos
            .FirstOrDefaultAsync(x => x.Uid == id);

        var client = await context.ClientsPhotos
            .FirstOrDefaultAsync(x => x.Uid == id);

        if (animal != null)
        {
            context.AnimalsPhotos.Remove(animal);
        }
        else if (exposition != null)
        {
            context.ExpositionsPhotos.Remove(exposition);
        }
        else if (client != null)
        {
            context.ClientsPhotos.Remove(client);
        }
        else
        {
            throw new ProcessException($"Photo (ID = {id}) not found.");
        }

        await context.SaveChangesAsync();
    }
}
