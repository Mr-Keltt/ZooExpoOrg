using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Animals.Animals;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;

    public AnimalService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper,
        IAppLogger logger
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<AnimalModel>> GetOwned(Guid ownerId)
    { 
        using var context = await dbContextFactory.CreateDbContextAsync();

        var owner = await context.Clients
            .Include(x => x.Animals)
            .FirstOrDefaultAsync(x => x.Uid == ownerId);

        if (owner == null)
        {
            throw new ProcessException($"Client (ownerID = {ownerId}) not found.");
        }

        return mapper.Map<IEnumerable<AnimalModel>>(owner.Animals);
    }

    public async Task<AnimalModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<AnimalModel>(animal);

        return result;
    }

    public async Task<AnimalModel> Create(CreateAnimalModel model)
    {
        using var context = dbContextFactory.CreateDbContext();

        var client = context.Clients.FirstOrDefault(x => x.Uid == model.OwnerId);

        if (client == null)
        {
            throw new ProcessException($"Client (ID = {model.OwnerId}) not found.");
        }

        var animal = mapper.Map<AnimalEntity>(model);

        context.Animals.Add(animal);

        client.Animals.Add(animal);

        context.SaveChanges();

        return mapper.Map<AnimalModel>(animal);
    }

    public async Task Update(Guid id, UpdateAnimalModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Uid == id);

        if (animal == null)
        {
            throw new ProcessException($"Animal (ID = {id}) not found.");
        }

        animal = mapper.Map(model, animal);

        context.Animals.Update(animal);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (animal == null)
        {
            throw new ProcessException($"Animal (ID = {id}) not found.");
        }

        context.Animals.Remove(animal);

        await context.SaveChangesAsync();
    }
}
