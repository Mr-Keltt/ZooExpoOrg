namespace ZooExpoOrg.Web.Services.Photos;

public class VueCreatePhotoModel
{
    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public string StringImageData { get; set; }
}