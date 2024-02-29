namespace ZooExpoOrg.Context.Entities;

public class Pet : Animal
{
    public int AnimalId { get; set; }
    public virtual Animal Animal { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public ICollection<Comment>? Comments { get; set; }
}
