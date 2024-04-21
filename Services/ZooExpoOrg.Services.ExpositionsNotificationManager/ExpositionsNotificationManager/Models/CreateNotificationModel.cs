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
    public Guid SenderNotificationId { get; set; }

    public virtual ICollection<Guid> RecipientsNotification { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public DateTime DepartureTime { get; set; }
}


public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<CreateNotificationModel, NotificationEntity>()

            .ForMember(dest => dest.SenderNotificationId, opt => opt.Ignore())
            .ForMember(dest => dest.RecipientsNotification, opt => opt.Ignore());

        CreateMap<ClientEntity, Guid>().ConvertUsing(client => client.Uid);
    }

    public class CreateNotificationModelActions : IMappingAction<CreateNotificationModel, NotificationEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateNotificationModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateNotificationModel source, NotificationEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var sender = db.Clients.FirstOrDefault(x => x.Uid == source.SenderNotificationId);
            var recipients = db.Clients.Where(x => source.RecipientsNotification.Contains(x.Uid)).ToList();

            if (sender == null || !(recipients.Any()))
            {
                throw new NullReferenceException();
            }

            destination.SenderNotificationId = sender.Id;
            destination.RecipientsNotification = recipients;
        }
    }
}

public class CreateNotificationModelValidator : AbstractValidator<CreateNotificationModel>
{
    public CreateNotificationModelValidator()
    {
        RuleFor(model => model.SenderNotificationId)
            .NotEmpty().WithMessage("SenderNotificationId is required.");
        RuleFor(model => model.RecipientsNotification)
            .NotEmpty().WithMessage("RecipientsNotification cannot be empty.");
        RuleForEach(model => model.RecipientsNotification)
            .NotEmpty().WithMessage("Each recipient ID must be specified.");
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.");
        RuleFor(model => model.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(10000).WithMessage("Description must be less than 10000 characters.");
        RuleFor(model => model.DepartureTime)
            .Must(BeAValidDateTime).WithMessage("DepartureTime must be a valid DateTime.");
    }

    private bool BeAValidDateTime(DateTime dateTime)
    {
        return dateTime > DateTime.UtcNow;
    }
}