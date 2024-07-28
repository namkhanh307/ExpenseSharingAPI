using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class PersonResponseModel
    {
        private readonly PersonResponseModel _responseModel;
        public PersonResponseModel()
        {
            
        }
        public PersonResponseModel(PersonResponseModel response)
        {
            _responseModel = response;
        }
        public PersonResponseModel(string _name, double _amount)
        {
            Name = _name;
            Amount = _amount;
        }
        public PersonResponseModel(string _id, string _name, double _amount, bool _isAdmin, bool _isShared, double _diff, double _fixed, double _flex, double _shared, string _expenseId)
        {
            Id = _id;
            IsShared = _isShared;
            Name = _name;
            Amount = _amount;
            IsAdmin = _isAdmin;
            Diff = _diff;
            Fixed = _fixed;
            Flex = _flex;
            Shared = _shared;
            ExpenseId = _expenseId;

        }
        //public string ReportId { get; set; }
        public string ExpenseId { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsShared { get; set; }
        public double Amount { get; set; }
        public double Diff{ get; set; }
        public double Fixed { get; set; }
        public double Flex { get; set; }
        public double Shared { get; set; }
    }
}
