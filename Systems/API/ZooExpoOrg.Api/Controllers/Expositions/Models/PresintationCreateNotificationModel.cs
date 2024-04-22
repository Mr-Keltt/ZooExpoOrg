using AutoMapper;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.ExpositionsNotificationManager;

namespace ZooExpoOrg.Api.Controllers.Expositions.Models;

public class PresintationCreateNotificationModel
{
    public string Title { get; set; }

    public string Text { get; set; }
}

public class PresintationCreateNotificationModelProfile : Profile
{
    public PresintationCreateNotificationModelProfile()
    {
        CreateMap<PresintationCreateNotificationModel, CreateNotificationModel>();
    }
}