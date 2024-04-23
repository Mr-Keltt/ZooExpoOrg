using ZooExpoOrg.Web.Common.Enumerables;

namespace ZooExpoOrg.Web.Services.Expositions;

public class VueCreateExpositionModel
{
    public Guid OrganizerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public AnimalType ParticipantsType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string? HouseNumber { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}