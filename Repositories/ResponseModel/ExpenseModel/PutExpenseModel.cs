
using Microsoft.AspNetCore.Http;

namespace Repositories.ResponseModel.ExpenseModel
{
    public class PutExpenseModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public double? Amount { get; set; }
        public IFormFile? InvoiceImage { get; set; }
    }
}
