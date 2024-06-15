namespace Repositories.Entities;

public class PersonGroup
{
    public required string PersonId { get; set; }
    public virtual required Person Person { get; set; }

    public required string GroupId { get; set; }
    public virtual required Group Group { get; set; }

    public bool IsAdmin { get; set; }
}