using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Expositions;

public class ExpositionService : IExpositionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public ExpositionService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<ExpositionModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var expositions = await context.Expositions.ToListAsync();

        return mapper.Map<IEnumerable<ExpositionModel>>(expositions);
    }

    public async Task<ExpositionModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<ExpositionModel>(exposition);
    }

    public async Task<ExpositionModel> Create(CreateExpositionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = mapper.Map<ExpositionEntity>(model);

        await context.Expositions.AddAsync(exposition);

        var client = await context.Clients.FirstOrDefaultAsync(x => x.Uid == model.OrganizerId);

        client.OrganizedExpositions.Append(exposition);

        await context.SaveChangesAsync();

        return mapper.Map<ExpositionModel>(exposition);
    }

    public async Task Update(Guid id, UpdateExpositionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        exposition = mapper.Map(model, exposition);

        context.Expositions.Update(exposition);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Guid>> GetAllSubscribers(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        return mapper.Map<ExpositionModel>(exposition).Subscribers;
    }

    public async Task Subscribe(Guid id, Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        var client = await context.Clients.FirstOrDefaultAsync(x => x.Uid == userId);

        if (client == null)
            throw new ProcessException($"Client (ID = {id}) not found.");

        exposition.Subscribers.Append(client);

        client.Subscriptions.Append(exposition);

        context.SaveChanges();
    }

    public async Task Unsubscribe(Guid id, Guid userId)
    {
        /*using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        var client = await exposition.Subscribers.FirstOrDefaultAsync(x => x.Uid == userId);

        if (client == null)
            throw new ProcessException($"Client (ID = {id}) is not subscribed to this exhibition.");

        var subscription = client.Subscriptions.Fi(exposition);

        exposition.Subscribers.Remove(client);

        context.SaveChanges();*/
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Guid>> GetAllParticipants(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Uid == id);

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        return mapper.Map<ExpositionModel>(exposition).Participants;
    }

    public async Task AddParticipant(Guid id, Guid animalId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteParticipant(Guid id, Guid animalId)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var exposition = await context.Expositions.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (exposition == null)
            throw new ProcessException($"Exposition (ID = {id}) not found.");

        context.Expositions.Remove(exposition);

        await context.SaveChangesAsync();
    }
}
