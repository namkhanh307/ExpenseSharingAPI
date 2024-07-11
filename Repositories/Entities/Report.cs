

namespace Repositories.Entities;

public partial class Report : BaseEntity
{
    public string? Name { get; set; }
    public string? GroupId { get; set; }
    //public string PersonId { get; set; }
    public virtual Group Group { get; set; } = null!;
    //public virtual Person Person { get; set; } = null!;
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
