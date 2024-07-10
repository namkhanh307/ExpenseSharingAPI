
namespace Repositories.Entities;

public partial class PersonGroup : BaseEntity
{
    public string PersonId { get; set; }

    public string GroupId { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
