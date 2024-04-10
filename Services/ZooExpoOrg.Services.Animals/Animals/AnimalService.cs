using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Animals;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AnimalService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AnimalModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animals = context.Animals
            .Include(x => x.Owner).ThenInclude(x => x.Photo)
            .Include(x => x.Comments).ThenInclude(x => x.Author)
            .Include(x => x.Photos)
            .Include(x => x.Achievements).ThenInclude(x => x.ConfirmationAchievement)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<AnimalModel>>(animals);

        return result;
    }

    public async Task<AnimalModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = context.Animals.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<AnimalModel>(animal);

        return result;
    }

    public async Task<AnimalModel> Create(CreateAnimalModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = mapper.Map<AnimalEntity>(model);

        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();

        return mapper.Map<AnimalModel>(animal);
    }

    public async Task Update(Guid id, UpdateAnimalModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Uid == id);

        animal = mapper.Map(model, animal);

        context.Animals.Update(animal);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = await context.Animals.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (animal == null)
            throw new ProcessException($"Animal (ID = {id}) not found.");

        context.Animals.Remove(animal);

        await context.SaveChangesAsync();
    }
}
