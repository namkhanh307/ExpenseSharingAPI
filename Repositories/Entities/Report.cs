using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Report : BaseEntity
{
    public string? Name { get; set; }

    public string? GroupId { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual Group? Group { get; set; }

    //public virtual ICollection<PersonExpense> PersonExpenses { get; set; } = new List<PersonExpense>();

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
