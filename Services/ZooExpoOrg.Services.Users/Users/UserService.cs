using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;

namespace ZooExpoOrg.Services.Users;

public class UserService : IUserService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public UserService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var users = await context.Users
            .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
            .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
            .Include(x => x.Comments)
            .Include(x => x.Photo)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<UserModel>>(users);

        return result;
    }

    public async Task<UserModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var users = await context.Users
            .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
            .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
            .Include(x => x.Comments)
            .Include(x => x.Photo)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<UserModel>(users);

        return result;
    }

    public async Task<UserModel> Create(CreateUserModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = mapper.Map<UserEntity>(model);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return mapper.Map<UserModel>(user);
    }

    public async Task Update(Guid id, UpdateUserModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(x => x.Uid == id);

        user = mapper.Map(model, user);

        context.Users.Update(user);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.Users.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (user == null)
            throw new ProcessException($"Userl (ID = {id}) not found.");

        context.Users.Remove(user);

        await context.SaveChangesAsync();
    }
}
