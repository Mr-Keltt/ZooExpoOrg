using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Services.Clients;

public class VueCreateClientModel
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDate { get; set; }
}