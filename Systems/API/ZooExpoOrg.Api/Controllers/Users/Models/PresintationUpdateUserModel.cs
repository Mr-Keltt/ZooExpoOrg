using AutoMapper;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Animals;
using ZooExpoOrg.Services.Users;

namespace ZooExpoOrg.Api.Controllers.Users; 

public class PresintationUpdateUserModel 
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Guid PhotoId { get; set; }

    //public virtual ICollection<ExpositionEntity> Subscriptions { get; set; }

    //public virtual ICollection<ExpositionEntity> OrganizedExpositions { get; set; }

    public virtual ICollection<AnimalModel> Animals { get; set; }

    //public virtual ICollection<Comment> Comments { get; set; }
}

public class UpdateUserModelProfile : Profile
{
    public UpdateUserModelProfile()
    {
        CreateMap<PresintationUpdateUserModel, UserModel>()
            .ReverseMap();
    }
}