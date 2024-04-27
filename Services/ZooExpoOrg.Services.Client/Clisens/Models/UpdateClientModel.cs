using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.Clients; 

public class UpdateClientModel 
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }
}

public class UpdateClientModelProfile : Profile
{
    public UpdateClientModelProfile()
    {
        CreateMap<UpdateClientModel, ClientEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Patronymic));
    }
}

public class UpdateClientModelValidator : AbstractValidator<UpdateClientModel>
{
    public UpdateClientModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname must not exceed 50 characters.");
        RuleFor(x => x.Patronymic)
            .MaximumLength(50).WithMessage("Patronymic must not exceed 50 characters.");
    }

    private bool BeValidGuidOrNull(Guid? photoId)
    {
        if (photoId == null)
            return true;

        return Guid.TryParse(photoId.ToString(), out _);
    }
}