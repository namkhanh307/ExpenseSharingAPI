namespace Repositories.Entities;

public class Group : BaseEntity
{
    public string? Name {get; set;}
    public int Size {get; set;}
    public int Type {get; set;}
    public virtual required ICollection<Report> Reports {get; set;}

}