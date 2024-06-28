using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities;

public class Record : BaseEntity
{
    public required string PersonId {get;set;}
    [ForeignKey("PersonID")]
    public virtual required Person Person{get; set;}
    public required string ExpenseId {get; set;}
    [ForeignKey("ExpenseID")]
    public virtual required Expense Expense {get; set;}
    public required string ReportId {get; set;} 
    [ForeignKey("ReportID")]
    public virtual required Report Report {get; set;}
}