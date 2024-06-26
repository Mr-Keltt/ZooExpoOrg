﻿namespace ZooExpoOrg.Services.Photos;

public interface IPhotoService
{
    public Task<IEnumerable<PhotoModel>> GetAllLocationById(Guid locationId);
    public Task<PhotoModel> GetById(Guid id);
    public Task<PhotoModel> Create(CreatePhotoModel model);
    public Task Delete(Guid id);
}
