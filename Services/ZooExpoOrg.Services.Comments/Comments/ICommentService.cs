namespace ZooExpoOrg.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetLocatedIn(Guid locationId);
    Task<CommentModel> GetById(Guid id);
    Task<CommentModel> Create(CreateCommentModel model);
    Task Update(Guid id, UpdateCommentModel model);
    Task Delete(Guid id);
}