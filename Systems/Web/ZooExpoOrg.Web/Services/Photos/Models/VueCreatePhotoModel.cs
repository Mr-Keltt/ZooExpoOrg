namespace ZooExpoOrg.Web.Services.Photos;

public class VueCreatePhotoModel
{
    public Guid OwnerId { get; set; }

	public Guid LocationId { get; set; }

    public string StringImageData { get; set; }
}