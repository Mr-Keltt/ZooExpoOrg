namespace ZooExpoOrg.Services.Clients;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

public class ClientService : IClientService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public ClientService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper, 
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<ClientModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var clients = await context.Clients.ToListAsync();

        return mapper.Map<IEnumerable<ClientModel>>(clients);
    }

    public async Task<ClientModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var clients = await context.Clients.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<ClientModel>(clients);
    }
    
    public async Task<ClientModel> Create(CreateClientModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var client = mapper.Map<ClientEntity>(model);

        await context.Clients.AddAsync(client);
        
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);

        user.ClientId = client.Uid;

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
