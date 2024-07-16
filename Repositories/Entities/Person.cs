using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Person
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual ICollection<PersonExpense> PersonExpenses { get; set; } = new List<PersonExpense>();

    public virtual ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
