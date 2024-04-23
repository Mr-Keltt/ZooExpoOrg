namespace ZooExpoOrg.Web.Services.Photos;

public class VueCreatePhotoModel
{
    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageMimeType { get; set; }
}