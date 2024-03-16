using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class ExpositionsCommentsRepository
{
    private readonly MainDbContext _dbContext;

    public ExpositionsCommentsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ExpositionComment>> Get()
    {
        return await _dbContext.ExpositionsComments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ExpositionComment?> GetById(int id)
    {
        return await _dbContext.ExpositionsComments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int expositionId,
        int authorId,
        string text,
        DateTime dateWriting)
    {
        ExpositionComment expositionComment = new ExpositionComment
        {
            AuthorId = authorId,
            Text = text,
            ExpositionId = expositionId,
            DateWriting = dateWriting
        };

        await _dbContext.AddAsync(expositionComment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string text,
        DateTime dateWriting)
    {
        await _dbContext.ExpositionsComments
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Text, text)
                .SetProperty(a => a.DateWriting, dateWriting)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.ExpositionsComments
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
