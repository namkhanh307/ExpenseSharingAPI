
namespace Repositories.Entities;

public partial class Group : BaseEntity
{
    public string? Name { get; set; }
    public int Size { get; set; }
    public int? Type { get; set; }
    public virtual ICollection<PersonExpense> PersonExpenses { get; set; } = new List<PersonExpense>();
    public virtual ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
