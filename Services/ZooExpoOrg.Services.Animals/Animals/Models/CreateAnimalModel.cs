using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;
using ZooExpoOrg.Common.Exceptions;

namespace ZooExpoOrg.Services.Animals;

public class CreateAnimalModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Breed { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public Guid OwnerId { get; set; }
}

public class CreateAnimalModelProfile : Profile
{
    public CreateAnimalModelProfile()
    {
        CreateMap<CreateAnimalModel, AnimalEntity>()
            .BeforeMap<CreateAnimalModelActions>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }

    public class CreateAnimalModelActions : IMappingAction<CreateAnimalModel, AnimalEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateAnimalModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(CreateAnimalModel source, AnimalEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Users.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            if (owner == null)
                throw new ProcessException($"Owner (ID = {source.OwnerId}) not found.");

            destination.OwnerId = owner.Id;
        }
    }
}