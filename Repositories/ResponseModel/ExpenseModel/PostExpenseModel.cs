
namespace Repositories.ResponseModel.ExpenseModel
{
    public class PostExpenseModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Amount { get; set; }
        public string? PersonId { get; set; }
        public string? ReportId { get; set; }
        public string? InvoiceImage { get; set; }
    }
}
