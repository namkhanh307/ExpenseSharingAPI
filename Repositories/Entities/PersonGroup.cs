using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities;

public class PersonGroup
{
    public required string PersonId { get; set; }
    [ForeignKey("PersonID")]
    public virtual required Person Person { get; set; }
    public required string GroupId { get; set; }
    [ForeignKey("GroupID")]
    public virtual required Group Group { get; set; }

    public bool IsAdmin { get; set; }
}