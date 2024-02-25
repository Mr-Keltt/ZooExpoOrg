namespace ZooExpoOrg.Context.Entities.Common;

public abstract class PdfFile : BaseEntity
{
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}
