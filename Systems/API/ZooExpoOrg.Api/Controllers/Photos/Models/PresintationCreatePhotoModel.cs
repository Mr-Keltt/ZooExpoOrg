using AutoMapper;
using ZooExpoOrg.Services.Photos;

namespace ZooExpoOrg.Api.Controllers.Photos;


public class PresintationCreatePhotoModel
{
    public Guid OwnerId;

    public Guid LocationId { get; set; }

	public string stringImageData { get; set; }
}

public class PresintationCreatePhotoModelProfile : Profile
{
    public PresintationCreatePhotoModelProfile()
    {
        CreateMap<PresintationCreatePhotoModel, CreatePhotoModel>();
    }
}