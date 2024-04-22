using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.ExpositionsNotificationManager;

public class CreateNotificationModel
{
    public string Title { get; set; }

    public string Text { get; set; }

    public DateTime DepartureTime { get; set; }
}


public class CreateNotificationModelProfile : Profile
{
    public CreateNotificationModelProfile()
    {
        CreateMap<CreateNotificationModel, NotificationEntity>()
            .ForMember(dest => dest.SenderId, opt => opt.Ignore())
            .ForMember(dest => dest.Recipients, opt => opt.Ignore());
    }
}

public class CreateNotificationModelValidator : AbstractValidator<CreateNotificationModel>
{
    public CreateNotificationModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be less than 100 characters.");
        RuleFor(model => model.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(10000).WithMessage("Description must be less than 10000 characters.");
        RuleFor(model => model.DepartureTime)
            .NotEmpty().WithMessage("DepartureTime is required.")
            .Must(BeAValidDate).WithMessage("DepartureTime must be a valid DateTime.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date < DateTime.Now && date > DateTime.Now.AddYears(-150);
    }
}