
namespace Repositories.ResponseModel.ExpenseModel
{
    public class GetExpenseModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Amount { get; set; }
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? InvoiceImage { get; set; }
    }
}
