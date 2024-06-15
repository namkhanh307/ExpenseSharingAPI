namespace Repositories.Entities;

public class Record : BaseEntity
{
    public required string PersonId {get;set;}
    public virtual required Person Person{get; set;}
    public required string ExpenseId {get; set;}
    public virtual required Expense Expense {get; set;}
    public required string ReportId {get; set;} 
    public virtual required Report Report {get; set;}
}