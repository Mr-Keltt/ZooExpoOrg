namespace ZooExpoOrg.Web.Services.Comments;

public class VueCommentModel
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}