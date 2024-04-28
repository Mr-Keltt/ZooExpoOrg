namespace ZooExpoOrg.Web.Pages.Exposition;

public class ExpositionValidationErorr
{
    public ExpositionValidationErorr()
    {
        Title = "";
        Description = "";
        ParticipantsType = "";
        Country = "";
        City = "";
        Street = "";
        HouseNumber = "";
        DateStart = "";
        DateEnd = "";
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string DateStart { get; set; }

    public string DateEnd { get; set; }
}