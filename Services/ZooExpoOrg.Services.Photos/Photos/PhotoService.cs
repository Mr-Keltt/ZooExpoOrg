﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Common.Extensions;
using ZooExpoOrg.Common.Validator;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;
using ZooExpoOrg.Services.Logger;

namespace ZooExpoOrg.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IModelValidator<CreatePhotoModel> createPhotoModelValidator;

    public PhotoService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper,
        IAppLogger logger,
        IModelValidator<CreatePhotoModel> createPhotoModelValidator
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.logger = logger;
        this.createPhotoModelValidator = createPhotoModelValidator;
    }

    public async Task<IEnumerable<PhotoModel>> GetAllLocationById(Guid locationId)
    {
        using var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == locationId);
        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == locationId);
        var client = await db.Clients.FirstOrDefaultAsync(x => x.Uid == locationId);

        ICollection<PhotoEntity> photos = null;

        if (animal != null)
        {
            photos = animal.Photos;
        }
        else if (exposition != null)
        {
            photos = exposition.Photos;
        }
        else if (client != null)
        {
            photos = client.Photos;
        }
        else
        {
            throw new ProcessException($"Photo not found.");
        }

        return mapper.Map<IEnumerable<PhotoModel>>(photos);
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photo = await context.Photos.FirstOrDefaultAsync(x => x.Uid == id);

        return mapper.Map<PhotoModel>(photo);
    }

    public async Task<PhotoModel> Create(CreatePhotoModel model)
    {
        createPhotoModelValidator.Check(model);

        using var db = dbContextFactory.CreateDbContext();

        var owner = db.Clients.FirstOrDefault(x => x.Uid == model.OwnerId);

        if (owner == null) 
        {
            throw new ProcessException($"User (ID = {model.OwnerId}) not found.");
        }

        var client = db.Clients.FirstOrDefault(x => x.Uid == model.LocationId);
        var animal = db.Animals.FirstOrDefault(x => x.Uid == model.LocationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == model.LocationId);

        ICollection<PhotoEntity> location;

        if (client != null)
        {
            location = client.Photos;
        }
        else if (animal != null)
        {
            location = animal.Photos;
		}
        else if (exposition != null)
        {
            location = exposition.Photos;
		}
        else
        {
            throw new ProcessException($"Location (ID = {model.LocationId}) not found.");
        }

        var photo = mapper.Map<PhotoEntity>(model);

        db.Photos.Add(photo);

        owner.Photos.Add(photo);

        if (client != null)
        {
            location.Clear();
			location.Add(photo);
		}
        else
        {
			location.Add(photo);
		}

        db.SaveChanges();

        return mapper.Map<PhotoModel>(photo);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photo = await context.Photos.FirstOrDefaultAsync(x => x.Uid == id);
        
        if (photo == null)
        {
            throw new ProcessException($"Photo (ID = {id}) not found.");
        }

        var owner = await context.Clients.FirstOrDefaultAsync(x => x.Id == photo.OwnerId);
        var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == photo.ClientId);
        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id == photo.AnimalId);
        var exposition = await context.Expositions.FirstOrDefaultAsync(x => x.Id == photo.ExpositionId);

        if (client != null)
        {
            client.Photos.Remove(photo);
        }
        else if (animal != null)
        {
            animal.Photos.Remove(photo);
        }
        else if (exposition != null)
        {
            exposition.Photos.Add(photo);
        }

        context.Photos.Remove(photo);

        owner.Photos.Remove(photo);

        await context.SaveChangesAsync();
    }
}
