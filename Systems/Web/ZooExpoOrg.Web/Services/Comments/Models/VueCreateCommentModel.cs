namespace ZooExpoOrg.Web.Services.Comments;

public class VueCreateCommentModel
{
    public Guid LocationId { get; set; }

    public Guid AuthorId { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}