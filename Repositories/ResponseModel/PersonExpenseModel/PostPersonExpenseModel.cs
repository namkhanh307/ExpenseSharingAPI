
using Repositories.ResponseModel.CalculateModel;
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class PostPersonExpenseModel
    {
        public required string ExpenseId { get; set; }
        public required List<PersonExpenseModel> Persons { get; set; }
        public required string ReportId { get; set; }

    }
}
