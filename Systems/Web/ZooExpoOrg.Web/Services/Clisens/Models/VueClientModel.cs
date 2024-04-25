using System.Collections.Generic;
using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Services.Clients;

public class VueClientModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Guid? PhotoId { get; set; }

    public virtual IEnumerable<Guid> Photos { get; set; }

    public virtual IEnumerable<Guid> OwnedPhotos { get; set; }

    public virtual IEnumerable<Guid> Subscriptions { get; set; }

    public virtual IEnumerable<Guid> OrganizedExpositions { get; set; }

    public virtual IEnumerable<Guid> Animals { get; set; }

    public virtual IEnumerable<Guid> Comments { get; set; }

    public virtual IEnumerable<Guid> UnreadNotifications { get; set; }
}