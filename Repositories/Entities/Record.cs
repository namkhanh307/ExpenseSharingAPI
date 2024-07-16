using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Record
{
    public string Id { get; set; } = null!;

    public string? PersonId { get; set; }

    public string? ExpenseId { get; set; }

    public string? ReportId { get; set; }

    public double? Amount { get; set; }

    public bool? IsPaid { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual Expense? Expense { get; set; }

    public virtual Person? Person { get; set; }

    public virtual Report? Report { get; set; }
}
