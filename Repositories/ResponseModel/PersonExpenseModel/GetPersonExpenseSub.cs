using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class GetPersonExpenseSubSub
    {
        public string? ExpenseId { get; set; }
        public string? ExpenseName { get; set; }
        public double? ExpenseAmount { get; set; }
        public List<string>? ExpenesePaidBy { get; set; }
        public DateTime ExpenseCreatedTime { get; set; }
        public List<GetPersonExpenseSubSubModel>? PersonExpenseSub {  get; set; }     
    }
}
