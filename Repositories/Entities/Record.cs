
namespace Repositories.Entities;

public partial class Record : BaseEntity
{
    public string PersonId { get; set; }

    public string ExpenseId { get; set; }

    public string ReportId { get; set; }

    public double? Amount { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Expense Expense { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;
}
