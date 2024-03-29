﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.Photos;

public class CreatePhotoModel
{
    public Guid OwnerId;
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
}

public class CreatePhotoModelProfile : Profile
{
    public CreatePhotoModelProfile()
    {
        CreateMap<CreatePhotoModel, AnimalPhotoEntity> ()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.AnimalId, opt => opt.Ignore());

        CreateMap<CreatePhotoModel, ExpositionPhotoEntity>()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.ExpositionId, opt => opt.Ignore());

        CreateMap<CreatePhotoModel, UserPhotoEntity>()
            .BeforeMap<CreatePhotoModelActions>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }

    public class CreatePhotoModelActions :
        IMappingAction<CreatePhotoModel, AnimalPhotoEntity>,
        IMappingAction<CreatePhotoModel, ExpositionPhotoEntity>,
        IMappingAction<CreatePhotoModel, UserPhotoEntity>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreatePhotoModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async void Process(CreatePhotoModel source, AnimalPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.Animals.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.AnimalId = owner.Id;
        }

        public async void Process(CreatePhotoModel source, ExpositionPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.ExpositionsPhotos.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.ExpositionId = owner.Id;
        }

        public async void Process(CreatePhotoModel source, UserPhotoEntity destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var owner = await db.UsersPhotos.FirstOrDefaultAsync(x => x.Uid == source.OwnerId);

            destination.UserId = owner.Id;
        }
    }
}
