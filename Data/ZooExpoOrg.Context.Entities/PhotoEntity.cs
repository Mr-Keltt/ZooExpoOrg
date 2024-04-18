using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class PhotoEntity : BaseEntity
{
    public int OwnerId { get; set; }
    public virtual ClientEntity Owner { get; set; }

    public int? ClientId { get; set; }
    public virtual ClientEntity? Client { get; set; }

    public int? AnimalId { get; set; }
    public virtual AnimalEntity? Animal { get; set; }

    public int? ExpositionId { get; set; }
    public virtual ExpositionEntity? Exposition { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageMimeType { get; set; }
}
