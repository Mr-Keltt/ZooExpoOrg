using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Context.Entities.Common;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Common.Extensions;

namespace ZooExpoOrg.Services.Users;

public class UserModel
{
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid PhotoId { get; set; }

    //public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    //public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalModel> Animals { get; set; }

    //public virtual ICollection<Comment> Comments { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .BeforeMap<UserModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Patronymic, opt => opt.Ignore());
    }

    public class UserModelActions : IMappingAction<UserEntity, UserModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UserModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(UserEntity source, UserModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var user = await db.Users
                .Include(x => x.Subscriptions).ThenInclude(x => x.Photos)
                .Include(x => x.OrganizedExpositions).ThenInclude(x => x.Photos)
                .Include(x => x.Comments)
                .Include(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == source.Id);

            destination.Id = user.Uid;
            destination.Patronymic = !user.Patronymic.IsNullOrEmpty() ? user.Patronymic : "";
        }
    }
}