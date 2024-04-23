namespace ZooExpoOrg.Web.Services.Photos;

public interface IPhotoService
{
    public Task<IEnumerable<VuePhotoModel>> GetPhotosOwned(Guid ownerId);
    public Task<VuePhotoModel> GetPhoto(Guid photoId);
    public Task AddPhoto(VueCreatePhotoModel model);
    public Task DeletePhoto(Guid photoId);
}
