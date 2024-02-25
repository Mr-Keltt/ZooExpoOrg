using ZooExpoOrg.Context.Entities.Common;

namespace ZooExpoOrg.Context.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Patronymic { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public UserPhoto? PhotoId { get; set; }
    public virtual UserPhoto Photo { get; set; }

    public virtual ICollection<Exposition>? Subscriptions { get; set; }

    public virtual ICollection<Exposition>? OrganizedExpositions { get; set; }

    public virtual ICollection<Pet>? Pets { get; set; }
}