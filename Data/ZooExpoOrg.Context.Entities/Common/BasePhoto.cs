namespace ZooExpoOrg.Context.Entities.Common;

public abstract class BasePhoto : BaseEntity
{
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
}
