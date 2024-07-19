
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class GetPersonExpenseModel
    {
        public string? ExpenseId { get; set; }
        public GetPersonModel? Person { get; set; }//person purchase for that expense
        public List<GetPersonModel>? Persons { get; set; }
        public string? ReportId { get; set; }

    }
}
