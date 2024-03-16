using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class ExpositionsRepository
{
    private readonly MainDbContext _dbContext;

    public ExpositionsRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Exposition>> Get()
    {
        return await _dbContext.Expositions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Exposition?> GetById(int id)
    {
        return await _dbContext.Expositions
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        int organizersId,
        string title,
        string description,
        string country,
        string city,
        string street,
        string? houseNumber,
        DateTime dateStart,
        DateTime dateEnd,
        List<ExpositionPhoto>? photos)
    {
        Exposition exposition = new Exposition
        {
            Title = title,
            Description = description,
            OrganizersId = organizersId,
            Country = country,
            City = city,
            Street = street,
            HouseNumber = houseNumber,
            DateStart = dateStart,
            DateEnd = dateEnd,
            Photos = photos
        };

        await _dbContext.AddAsync(exposition);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string title,
        string description,
        string country,
        string city,
        string street,
        string? houseNumber,
        DateTime dateStart,
        DateTime dateEnd,
        List<ExpositionPhoto>? photos,
        List<ExpositionComment>? comments,
        List<User>? subscribers)
    {
        await _dbContext.Expositions
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Title, title)
                .SetProperty(e => e.Description, description)
                .SetProperty(e => e.Country, country)
                .SetProperty(e => e.City, city)
                .SetProperty(e => e.Street, street)
                .SetProperty(e => e.HouseNumber, houseNumber)
                .SetProperty(e => e.DateStart, dateStart)
                .SetProperty(e => e.DateEnd, dateEnd)
                .SetProperty(e => e.Photos, photos)
                .SetProperty(e => e.Comments, comments)
                .SetProperty(e => e.Subscribers, subscribers)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.Expositions
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
