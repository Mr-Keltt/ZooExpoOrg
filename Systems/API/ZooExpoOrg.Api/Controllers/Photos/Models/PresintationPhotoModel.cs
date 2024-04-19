﻿using AutoMapper;
using ZooExpoOrg.Services.Photos;

namespace ZooExpoOrg.Api.Controllers.Photos;


public class PresintationPhotoModel
{
    public Guid Id;

    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageMimeType { get; set; }
}

public class PresintationPhotoModelProfile : Profile
{
    public PresintationPhotoModelProfile()
    {
        CreateMap<PhotoModel, PresintationPhotoModel>();
    }
}