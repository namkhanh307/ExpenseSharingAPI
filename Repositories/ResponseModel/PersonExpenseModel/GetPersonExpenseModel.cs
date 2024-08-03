
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonGroupModel;
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class GetPersonExpenseModel
    {      
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public List<GetPersonModel>? Persons { get; set; }
        public List<GetPersonExpenseSubSub>? PersonSubs { get; set; }

    }
}
