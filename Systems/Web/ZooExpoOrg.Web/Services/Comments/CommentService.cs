using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Clients;

namespace ZooExpoOrg.Web.Services.Comments;

public class CommentService : ICommentService
{
    private readonly HttpClient httpClient;

    public CommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VueCommentModel>> GetCommentsLocated(Guid locationId)
    {
        var response = await httpClient.GetAsync($"v1/comment/located/{locationId:Guid}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VueCommentModel>>() ?? new List<VueCommentModel>();
    }

    public async Task<VueCommentModel> GetComment(Guid commentId)
    {
        var response = await httpClient.GetAsync($"v1/comment/{commentId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VueCommentModel>() ?? new();
    }

    public async Task AddComment(VueCreateCommentModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/comment", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task UpdateComment(Guid commentId, VueUpdateCommentModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/comment/{commentId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteComment(Guid commentId)
    {
        var response = await httpClient.DeleteAsync($"v1/comment/{commentId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}