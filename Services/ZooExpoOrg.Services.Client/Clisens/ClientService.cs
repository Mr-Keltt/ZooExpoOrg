namespace ZooExpoOrg.Services.Clients;

using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.Settings;

public class ClientService : IClientService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly DbSettings dbSettings;
    private readonly IModelValidator<CreateClientModel> createClientModelValidator;
    private readonly IModelValidator<UpdateClientModel> updateClientModelValidator;

    public ClientService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper, 
        IAppLogger logger,
        DbSettings dbSettings,
        IModelValidator<CreateClientModel> createClientModelValidator,
        IModelValidator<UpdateClientModel> updateClientModelValidator
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
        this.dbSettings = dbSettings;
        this.createClientModelValidator = createClientModelValidator;
        this.updateClientModelValidator = updateClientModelValidator;
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
        createClientModelValidator.Check(model);

        using var context = dbContextFactory.CreateDbContext();

        var user = context.Users.FirstOrDefault(x => x.Id == model.UserId);

        if (user == null)
        {
            throw new ProcessException($"Accounts (ID = {model.UserId}) not found.");
        }

        if (user.UserName == dbSettings.Init.Administrator.UserName)
        {
            throw new ProcessException($"Accounts (ID = {model.UserId}) is an adminstrator.");
        }

        if (user.ClientId != null)
        {
            throw new ProcessException($"Accounts (ID = {model.UserId}) is already in use");
        }

        var client = mapper.Map<ClientEntity>(model);

        context.Clients.Add(client);

        user.ClientId = client.Uid;

        context.SaveChanges();

        return mapper.Map<ClientModel>(client);
    }

    public async Task Update(Guid id, UpdateClientModel model)
    {
        updateClientModelValidator.Check(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var client = await context.Clients.FirstOrDefaultAsync(x => x.Uid == id);

        if (client == null)
        {
            throw new ProcessException($"Client (ID = {id}) not found.");
        }

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
