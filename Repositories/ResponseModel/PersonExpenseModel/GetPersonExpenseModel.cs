
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class GetPersonExpenseModel
    {
        public string? ExpenseId { get; set; }
        public GetPersonModel? Person { get; set; }
        public List<GetPersonModel>? Persons { get; set; }
    }
}
