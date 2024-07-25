

using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class PutPersonExpenseModel
    {
        public required List<PostPersonExpenseSub> Persons { get; set; }
        public required string ReportId { get; set; }
    }
}
