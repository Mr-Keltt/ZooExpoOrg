using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Comments;

namespace ZooExpoOrg.Web.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly HttpClient httpClient;

    public PhotoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VuePhotoModel>> GetPhotosOwned(Guid ownerId)
    {
        var response = await httpClient.GetAsync($"v1/photo/owned/{ownerId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VuePhotoModel>>() ?? new List<VuePhotoModel>();
    }

    public async Task<VuePhotoModel> GetPhoto(Guid photoId)
    {
        var response = await httpClient.GetAsync($"v1/photo/{photoId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VuePhotoModel>() ?? new();
    }

    public async Task AddPhoto(VueCreatePhotoModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/photo", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeletePhoto(Guid photoId)
    {
        var response = await httpClient.DeleteAsync($"v1/photo/{photoId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
