﻿using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class ExpositionPhoto : BasePhoto
{
    public int ExpositionId { get; set; }
    public virtual Exposition Exposition { get; set; }
}
