﻿
namespace Repositories.ResponseModel.RecordModel
{
    public class GetRecordModel
    {
        public string? PersonId { get; set; }

        public string? ExpenseId { get; set; }

        public string? ReportId { get; set; }

        public double? Amount { get; set; }

        public bool? IsPaid { get; set; }
    }
}
