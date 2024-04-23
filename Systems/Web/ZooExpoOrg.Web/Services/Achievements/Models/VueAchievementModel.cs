namespace ZooExpoOrg.Web.Services.Achievements;

public class VueAchievementModel
{
    public Guid Id { get; set; }

    public Guid AnimalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateAward { get; set; }
}