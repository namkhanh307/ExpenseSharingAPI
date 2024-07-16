using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Group
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public int Size { get; set; }

    public int? Type { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
