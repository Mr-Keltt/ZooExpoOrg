using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Pages.Client;

public class ClientValidationErorr
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Gender { get; set; }

    public string BirthDate { get; set; }

    public ClientValidationErorr()
    {
        Name = "";
        Surname = "";
        Patronymic = "";
        Gender = "";
        BirthDate = "";
    }
}
