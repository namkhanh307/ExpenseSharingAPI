
namespace Repositories.ResponseModel.ExpenseModel
{
    public class PutExpenseModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Amount { get; set; }
        public string? CreatedBy { get; set; } //person who purchase that expense
        public string? ReportId { get; set; }
        public string? InvoiceImage { get; set; }
    }
}
