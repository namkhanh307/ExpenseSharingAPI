
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class PostPersonExpenseModel
    {
        public required string ExpenseId { get; set; }
        public required List<string> PersonIds { get; set; }
        public required string ReportId { get; set; }

    }
}
