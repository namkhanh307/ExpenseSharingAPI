﻿
namespace Repositories.ResponseModel.ExpenseModel
{
    public class PutExpenseModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Amount { get; set; }
        public string? ReportId { get; set; }
        public string? InvoiceImage { get; set; }
    }
}
