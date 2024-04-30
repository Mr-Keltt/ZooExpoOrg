using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly HttpClient httpClient;

    public PhotoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VuePhotoModel>>> GetPhotosLocated(Guid locationId)
    {
        var response = await httpClient.GetAsync($"v1/photo/located/{locationId}");

        var getResultHelper = new GetResultHelper<List<VuePhotoModel>>();

        return await getResultHelper.GetGetModelResult(response, "Photos");
    }

    public async Task<GetModelResult<VuePhotoModel>> GetPhoto(Guid photoId)
    {
        var response = await httpClient.GetAsync($"v1/photo/{photoId}");

        var getResultHelper = new GetResultHelper<VuePhotoModel>();

        return await getResultHelper.GetGetModelResult(response, "Photo");
    }

    public async Task<ManageModelResult<VuePhotoModel>> AddPhoto(VueCreatePhotoModel model)
    {
		var requestContent = JsonContent.Create(model);

        var response = await httpClient.PostAsync("v1/photo", requestContent);

        var getResultHelper = new GetResultHelper<VuePhotoModel>();

        return await getResultHelper.GetManageModelResult(response, "Photo add");
    }

    public async Task<DeleteModelResult> DeletePhoto(Guid photoId)
    {
        var response = await httpClient.DeleteAsync($"v1/photo/{photoId}");

        var getResultHelper = new GetResultHelper<VuePhotoModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Photo");
    }
}
