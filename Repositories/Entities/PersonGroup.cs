using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class PersonGroup
{
    public string PersonId { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
