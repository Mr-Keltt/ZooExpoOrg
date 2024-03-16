using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class AnimalsPhotosRepository
{
    private readonly MainDbContext _dbContext;

    public AnimalsPhotosRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AnimalPhoto>> Get()
    {
        return await _dbContext.AnimalsPhotos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AnimalPhoto?> GetById(int id)
    {
        return await _dbContext.AnimalsPhotos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int animalId,
        byte[] imageData,
        string imageMimeType)
    {
        AnimalPhoto animalPhoto = new AnimalPhoto
        {
            AnimalId = animalId,
            ImageMimeType = imageMimeType,
            ImageData = imageData
        };

        await _dbContext.AddAsync(animalPhoto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.AnimalsPhotos
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
