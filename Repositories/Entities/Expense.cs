namespace Repositories.Entities;

public class Expense : BaseEntity
{
    public required string Name  { get; set; }
    public required string Type {get; set;}
    public required double Amount {get; set;}
    public int? Month {get; set;}
    public int? Year {get; set;}
    public required string ReportId {get; set;}
    public virtual required Report Report{get; set;}
    public required string PersonId {get; set;}
    public virtual required Person Person {get; set;}
}