using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class ExpositionsPhotosRepository
{
    private readonly MainDbContext _dbContext;

    public ExpositionsPhotosRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ExpositionPhoto>> Get()
    {
        return await _dbContext.ExpositionsPhotos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ExpositionPhoto?> GetById(int id)
    {
        return await _dbContext.ExpositionsPhotos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int expositionId,
        byte[] imageData,
        string imageMimeType)
    {
        ExpositionPhoto expositionPhoto = new ExpositionPhoto
        {
            ExpositionId = expositionId,
            ImageMimeType = imageMimeType,
            ImageData = imageData
        };

        await _dbContext.AddAsync(expositionPhoto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.ExpositionsPhotos
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
