using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities;

public class Person
{
    [Key]
    public required string Id {get; set;}
    public required string Name {get; set;}
    public required string Phone {get; set;}
    public required string Password {get; set;}
    public virtual required ICollection<Expense> Expenses {get; set;}
    public virtual required ICollection<Item>? Items { get; set; }
    public virtual required ICollection<Record> Records {get; set;}
    public virtual required ICollection<PersonGroup> PersonGroups {get; set;}
    public virtual required ICollection<Report>? Reports { get; set; }

}