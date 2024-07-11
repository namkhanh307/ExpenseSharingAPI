
namespace Repositories.Entities;

public partial class PersonExpense : BaseEntity
{
    public string? ExpenseId { get; set; }

    public string? PersonId { get; set; }

    public string? GroupId { get; set; }

    public virtual Expense Expense { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
