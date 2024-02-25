using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class Comment : BaseEntity
{
    public User AuthorId { get; set; }
    public virtual User User { get; set; }

    public string Text { get; set; }

    public DateTime DateWriting { get; set; }
}
