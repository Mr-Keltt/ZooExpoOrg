using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;
using static System.Net.Mime.MediaTypeNames;

namespace ZooExpoOrg.Context.Repositories;

public class AnimalsCommentsRepository
{
    private readonly MainDbContext _dbContext;

    public AnimalsCommentsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AnimalComment>> Get()
    {
        return await _dbContext.AnimalsComments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AnimalComment?> GetById(int id)
    {
        return await _dbContext.AnimalsComments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int animalId,
        int authorId,
        string text,
        DateTime dateWriting)
    {
        AnimalComment animalComment = new AnimalComment
        {
            AuthorId = authorId,
            Text = text,
            AnimalId = animalId,
            DateWriting = dateWriting
        };

        await _dbContext.AddAsync(animalComment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string text,
        DateTime dateWriting)
    {
        await _dbContext.AnimalsComments
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Text, text)
                .SetProperty(a => a.DateWriting, dateWriting)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.AnimalsComments
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
