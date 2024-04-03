namespace ZooExpoOrg.Services.Clients;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;

public class ClientService : IClientService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public ClientService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ClientModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var clients = await context.Clients
            .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
            .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
            .Include(x => x.Comments)
            .Include(x => x.Photo)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<ClientModel>>(clients);

        return result;
    }

    public async Task<ClientModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var clients = await context.Clients
            .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
            .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
            .Include(x => x.Comments)
            .Include(x => x.Photo)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<ClientModel>(clients);

        return result;
    }

    public async Task<ClientModel> Create(CreateClientModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var client = mapper.Map<ClientEntity>(model);

        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();

        return mapper.Map<ClientModel>(client);
    }

    public async Task Update(Guid id, UpdateClientModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var client = await context.Clients.FirstOrDefaultAsync(x => x.Uid == id);

        client = mapper.Map(model, client);

        context.Clients.Update(client);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var client = await context.Clients.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (client == null)
            throw new ProcessException($"Client (ID = {id}) not found.");

        context.Clients.Remove(client);

        await context.SaveChangesAsync();
    }
}
