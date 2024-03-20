using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.Animals;

namespace ZooExpoOrg.Services.Animal.Animals;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AnimalService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<AnimalModel> Create(CreateAnimalModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animals = context.Animals
            .Include(x => x.User).ThenInclude(x => x.Photo)
            .Include(x => x.Comments).ThenInclude(x => x.User)
            .Include(x => x.Photos)
            .Include(x => x.Achievements).ThenInclude(x => x.ConfirmationAchievement)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<AnimalModel>>(animals);

        return result;
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animal = context.Animals.FirstOrDefaultAsync(x => x.Uid == id);

        var result = new AnimalModel()
        {
            Id = animal.Uid,
            Name = animal.Name,
            Description = animal.Description,
            Breed = animal.Breed,
            Gender = animal.Gender,
            BirthDate = animal.BirthDate,
            Height = animal.Height,
            Weight = animal.Weight,
            OwnerId = animal.Weight
        };

        return result;
    }

    public async Task<IEnumerable<AnimalModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<AnimalModel> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Guid id, UpdateAnimalModel model)
    {
        throw new NotImplementedException();
    }
}
