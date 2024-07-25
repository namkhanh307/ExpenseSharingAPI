
namespace Repositories.ResponseModel.RecordModel
{
    public class GetRecordModel
    {
        public string? Id { get; set; }

        public string? PersonId { get; set; }

        public string? PersonName { get; set; }

        public string? ExpenseId { get; set; }

        public string? ExpenseName { get; set; }

        public string? ReportId { get; set; }

        public string? ReportName { get; set; }

        public double? Amount { get; set; }

        public bool? IsPaid { get; set; }
    }
}
