using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculatedModel
    {
        public CalculatedModel(PersonResponseModel _person1, PersonResponseModel _person2, double _debt, string _expenseId)
        {
            Person1 = _person1;
            Person2 = _person2;
            Debt = _debt;
            ExpenseId = _expenseId;

        }
        public PersonResponseModel Person1 { get; set; } = new PersonResponseModel();
        public PersonResponseModel Person2 { get; set; } = new PersonResponseModel();

        public double Debt { get; set; } = double.MinValue;
        public string ExpenseId { get; set; }
    }
}
