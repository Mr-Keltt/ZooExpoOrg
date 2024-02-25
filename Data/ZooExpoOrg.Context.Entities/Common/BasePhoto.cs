using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooExpoOrg.Context.Entities.Common;

public abstract class BasePhoto : BaseEntity
{
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
}
