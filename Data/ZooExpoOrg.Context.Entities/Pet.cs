namespace ZooExpoOrg.Context.Entities;

public class Pet : Animal
{
    public Animal AnimalId { get; set; }
    public virtual Animal Animal { get; set; }

    public User UserId { get; set; }
    public virtual User User { get; set; }

    public ICollection<Comment>? Comments { get; set; }
}
