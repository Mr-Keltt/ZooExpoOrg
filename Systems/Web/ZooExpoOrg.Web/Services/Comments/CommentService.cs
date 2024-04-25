using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Clients;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Comments;

public class CommentService : ICommentService
{
    private readonly HttpClient httpClient;

    public CommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VueCommentModel>>> GetCommentsLocated(Guid locationId)
    {
        var response = await httpClient.GetAsync($"v1/comment/located/{locationId:Guid}");
        
        var getResultHelper = new GetResultHelper<List<VueCommentModel>>();

        return await getResultHelper.GetGetModelResult(response, "Comments");
    }

    public async Task<GetModelResult<VueCommentModel>> GetComment(Guid commentId)
    {
        var response = await httpClient.GetAsync($"v1/comment/{commentId}");

        var getResultHelper = new GetResultHelper<VueCommentModel>();

        return await getResultHelper.GetGetModelResult(response, "Comment");
    }

    public async Task<ManageModelResult<VueCommentModel>> AddComment(VueCreateCommentModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/comment", requestContent);

        var getResultHelper = new GetResultHelper<VueCommentModel>();

        return await getResultHelper.GetManageModelResult(response, "Comment add");
    }

    public async Task<ManageModelResult<VueCommentModel>> UpdateComment(Guid commentId, VueUpdateCommentModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/comment/{commentId}", requestContent);

        var getResultHelper = new GetResultHelper<VueCommentModel>();

        return await getResultHelper.GetManageModelResult(response, "Comment update");
    }

    public async Task<DeleteModelResult> DeleteComment(Guid commentId)
    {
        var response = await httpClient.DeleteAsync($"v1/comment/{commentId}");

        var getResultHelper = new GetResultHelper<VueCommentModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Comment");
    }
}