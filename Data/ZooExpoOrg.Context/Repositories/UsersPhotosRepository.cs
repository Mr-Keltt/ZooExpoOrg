using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class UsersPhotosRepository
{
    private readonly MainDbContext _dbContext;

    public UsersPhotosRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserPhoto>> Get()
    {
        return await _dbContext.UsersPhotos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserPhoto?> GetById(int id)
    {
        return await _dbContext.UsersPhotos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int userId,
        byte[] imageData,
        string imageMimeType)
    {
        UserPhoto userPhoto = new UserPhoto
        {
            UserId = userId,
            ImageMimeType = imageMimeType,
            ImageData = imageData
        };

        await _dbContext.AddAsync(userPhoto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.UsersPhotos
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
