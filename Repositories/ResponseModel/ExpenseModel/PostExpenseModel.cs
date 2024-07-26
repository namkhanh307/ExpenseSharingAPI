
using Microsoft.AspNetCore.Http;

namespace Repositories.ResponseModel.ExpenseModel
{
    public class PostExpenseModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Amount { get; set; }
        public string ReportId { get; set; }
        public IFormFile? InvoiceImage { get; set; }
    }
}
