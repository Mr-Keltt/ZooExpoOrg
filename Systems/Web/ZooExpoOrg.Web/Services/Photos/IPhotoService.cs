using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Photos;

public interface IPhotoService
{
    public Task<GetModelResult<List<VuePhotoModel>>> GetPhotosLocated(Guid ownerId);
    public Task<GetModelResult<VuePhotoModel>> GetPhoto(Guid photoId);
    public Task<ManageModelResult<VuePhotoModel>> AddPhoto(VueCreatePhotoModel model);
    public Task<DeleteModelResult> DeletePhoto(Guid photoId);
}
