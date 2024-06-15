namespace Repositories.Entities;

public class Expense : BaseEntity
{
    public required string Name  { get; set; }
    public required string Type {get; set;}
    public required double Amount {get; set;}
    public DateTime Time {get; set;}
    public required string ReportId {get; set;}
    public virtual required Report Report{get; set;}
    public required string PersonId {get; set;}
    public virtual required Person Person {get; set;}
    public virtual required ICollection<Record> Records {get; set;}
}