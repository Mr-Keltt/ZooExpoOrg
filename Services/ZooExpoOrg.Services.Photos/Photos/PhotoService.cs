using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

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
                                .Include(x => x.Animal)
                                .Where(x => x.Animal.Uid == OwnerId)
                                .ToListAsync();

        var expositionPhotos = await db.ExpositionsPhotos
                                    .Include(x => x.Exposition)
                                    .Where(x => x.Exposition.Uid == OwnerId)
                                    .ToListAsync();

        var userPhotos = await db.UsersPhotos
                            .Include(x => x.User)
                            .Where(x => x.User.Uid == OwnerId)
                            .ToListAsync();

        var result = mapper.Map<IEnumerable<PhotoModel>>(animalPhotos)
            .Concat(mapper.Map<IEnumerable<PhotoModel>>(expositionPhotos))
            .Concat(mapper.Map<IEnumerable<PhotoModel>>(userPhotos));

        return result;
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var db = dbContextFactory.CreateDbContext();

        var animalPhotos = await db.AnimalsPhotos
                                .Include(x => x.Animal)
                                .FirstOrDefaultAsync(x => x.Uid == id);

        var expositionPhotos = await db.ExpositionsPhotos
                                    .Include(x => x.Exposition)
                                    .FirstOrDefaultAsync(x => x.Uid == id);

        var userPhotos = await db.UsersPhotos
                            .Include(x => x.User)
                            .FirstOrDefaultAsync(x => x.Uid == id);

        if (animalPhotos != null)
            return mapper.Map<PhotoModel>(animalPhotos);
        else if (expositionPhotos != null)
            return mapper.Map<PhotoModel>(expositionPhotos);
        else if (userPhotos != null)
            return mapper.Map<PhotoModel>(userPhotos);
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

        var user = await context.Users
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
        else if (user != null)
        {
            var photo = mapper.Map<UserPhotoEntity>(model);
            await context.UsersPhotos.AddAsync(photo);

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

        var user = await context.UsersPhotos
            .FirstOrDefaultAsync(x => x.Uid == id);

        if (animal != null)
        {
            context.AnimalsPhotos.Remove(animal);
        }
        else if (exposition != null)
        {
            context.ExpositionsPhotos.Remove(exposition);
        }
        else if (user != null)
        {
            context.UsersPhotos.Remove(user);
        }
        else
        {
            throw new ProcessException($"Photo (ID = {id}) not found.");
        }

        await context.SaveChangesAsync();
    }
}
