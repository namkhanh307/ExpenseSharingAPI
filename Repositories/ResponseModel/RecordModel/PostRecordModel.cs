using Microsoft.AspNetCore.Http;

namespace Repositories.ResponseModel.RecordModel
{
    public class PostRecordModel
    {
        public string PersonId { get; set; }

        public string ExpenseId { get; set; }

        public IFormFile InvoiceImage { get; set; }

        public double? Amount { get; set; }

        public bool? IsPaid { get; set; }
    }
}
