
namespace Repositories.Entities;

public class Report : BaseEntity
{
    public string? Name {get; set;}
    public required string  PersonId {get; set;}
    public virtual required Person Person {get; set;}
    public required string GroupId {get; set;}
    public virtual required Group Group {get; set;}
    public virtual required ICollection<Item> Items {get; set;}
    public virtual required ICollection<Record> Records {get; set;}
}
