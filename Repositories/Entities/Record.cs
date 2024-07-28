using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Record : BaseEntity
{
    public string? PersonPayId { get; set; }
    public string? PersonReceiveId { get; set; }

    //public string? ExpenseId { get; set; }

    public string? ReportId { get; set; }

    public double? Amount { get; set; }

    public string? InvoiceImage {  get; set; }

    public bool? IsPaid { get; set; }
    public virtual Expense? Expense { get; set; }

    public virtual Person? PersonPay { get; set; }
    public virtual Person? PersonReceive { get; set; }

    public virtual Report? Report { get; set; }
}
