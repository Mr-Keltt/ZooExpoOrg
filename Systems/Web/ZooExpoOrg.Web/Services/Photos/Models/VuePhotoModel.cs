namespace ZooExpoOrg.Web.Services.Photos;

public class VuePhotoModel
{
    public Guid Id;

    public Guid OwnerId;

    public Guid LocationId { get; set; }

    public string StringImageData { get; set; }
}