using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Repositories;

public class UsersRepository
{
    private readonly MainDbContext _dbContext;

    public UsersRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> Get()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(
        string login,
        string password,
        string email,
        string name,
        string surname,
        string? patronymic,
        Gender gender,
        DateTime? birthDate)
    {
        User user = new User
        {
            Login = login,
            Password = password,
            Email = email,
            Name = name,
            Surname = surname,
            Patronymic = patronymic,
            Gender = gender,
            BirthDate = birthDate
        };

        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string login,
        string password,
        string email,
        string name,
        string surname,
        string? patronymic,
        Gender gender,
        DateTime? birthDate,
        int? photoId,
        List<Exposition>? subscriptions,
        List<Exposition>? organizedExpositions,
        List<Animal>? animals,
        List<Comment>? comments)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Login, login)
                .SetProperty(u => u.Password, password)
                .SetProperty(u => u.Email, email)
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.Surname, surname)
                .SetProperty(u => u.Patronymic, patronymic)
                .SetProperty(u => u.Gender, gender)
                .SetProperty(u => u.BirthDate, birthDate)
                .SetProperty(u => u.PhotoId, photoId)
                .SetProperty(u => u.Subscriptions, subscriptions)
                .SetProperty(u => u.OrganizedExpositions, organizedExpositions)
                .SetProperty(u => u.Animals, animals)
                .SetProperty(u => u.Comments, comments)
            );
    }

    public async Task DeleteById(int id)
    {
        await _dbContext.Users
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}
