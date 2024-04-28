using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Pages.Animals;

public class AnimalValidationErorr
{
    public AnimalValidationErorr()
    {
        Name = "";
        Description = "";
        Type = "";
        Gender = "";
        BirthDate = "";
        Height = "";
        Weight = "";
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }

    public string Gender { get; set; }

    public string BirthDate { get; set; }

    public string Height { get; set; }

    public string Weight { get; set; }
}