
namespace Repositories.Entities;

public partial class Expense : BaseEntity
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public double? Amount { get; set; }
    public string? PersonId { get; set; }
    public string? ReportId { get; set; }
    public string? InvoiceImage { get; set; }
    public virtual Person Person { get; set; } = null!;
    public virtual Report Report { get; set; } = null!;
    public virtual ICollection<PersonExpense> PersonExpenses { get; set; } = new List<PersonExpense>();
    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

}
