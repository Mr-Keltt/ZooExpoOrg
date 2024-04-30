using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Comments;

public interface ICommentService
{
    Task<GetModelResult<List<VueCommentModel>>> GetCommentsLocated(Guid locationId);
    Task<GetModelResult<VueCommentModel>> GetComment(Guid commentId);
    Task<ManageModelResult<VueCommentModel>> AddComment(VueCreateCommentModel model);
    Task<ManageModelResult<VueCommentModel>> UpdateComment(Guid commentId, VueUpdateCommentModel model);
    Task<DeleteModelResult> DeleteComment(Guid commentId);
}