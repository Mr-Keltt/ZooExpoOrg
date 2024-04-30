using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.ExpositionsNotificationManager;

namespace ZooExpoOrg.Api.Controllers.Clients;

public class PresintationNotificationModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public Guid SenderId { get; set; }

    public virtual ICollection<Guid> Recipients { get; set; }

    public DateTime DepartureTime { get; set; }
}

public class PresintationNotificationModelProfile : Profile
{
    public PresintationNotificationModelProfile()
    {
        CreateMap<NotificationModel, PresintationNotificationModel>();
    }
}
