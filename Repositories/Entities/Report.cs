
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities;

public class Report : BaseEntity
{
    public string? Name {get; set;}
    public required string  PersonId {get; set;}
    [ForeignKey("PersonID")]
    public virtual required Person Person {get; set;}
    public required string GroupId {get; set;}
    [ForeignKey("GroupID")]
    public virtual required Group Group {get; set;}
    public virtual required ICollection<Expense> Expenses {get; set;}
    public virtual required ICollection<Item> Items {get; set;}
    public virtual required ICollection<Record> Records {get; set;}
}
