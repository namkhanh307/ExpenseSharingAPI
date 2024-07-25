
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class GetPersonExpenseModel
    {
        public string? ExpenseId { get; set; }
        public string? ExpenseName { get; set; }
        public List<GetPersonExpenseSub>? Persons { get; set; }
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
    }
}
