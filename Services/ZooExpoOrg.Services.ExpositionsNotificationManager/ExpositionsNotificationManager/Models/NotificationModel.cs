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

public class NotificationModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public Guid SenderId { get; set; }

    public virtual ICollection<Guid> Recipients { get; set; }

    public DateTime DepartureTime { get; set; }
}


public class NotificationModelProfile : Profile
{
    public NotificationModelProfile()
    {
        CreateMap<NotificationEntity, NotificationModel > ()
            .BeforeMap<NotificationModelActions>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.SenderId, opt => opt.Ignore())
            .ForMember(dest => dest.Recipients, opt => opt.Ignore());
    }

    public class NotificationModelActions : IMappingAction<NotificationEntity, NotificationModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public NotificationModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(NotificationEntity source, NotificationModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var notification = db.Notifications.FirstOrDefault(x => x.Id == source.Id);

            if (notification == null)
            {
                throw new NullReferenceException();
            }

            foreach(var client in source.Recipients)
            {
                destination.Recipients.Add(client.Uid);
            }

            var sender = db.Expositions.FirstOrDefault(x => x.Id == source.SenderId);

            if (sender == null)
            {
                throw new NullReferenceException();
            }

            destination.SenderId = sender.Uid;
        }
    }
}