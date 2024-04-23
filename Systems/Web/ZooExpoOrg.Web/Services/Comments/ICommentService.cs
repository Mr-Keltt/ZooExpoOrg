namespace ZooExpoOrg.Web.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<VueCommentModel>> GetCommentsLocated(Guid locationId);
    Task<VueCommentModel> GetComment(Guid commentId);
    Task AddComment(VueCreateCommentModel model);
    Task UpdateComment(Guid commentId, VueUpdateCommentModel model);
    Task DeleteComment(Guid commentId);
}