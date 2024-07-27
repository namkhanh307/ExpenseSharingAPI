using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class PersonResponseModel
    {
        public PersonResponseModel()
        {
            
        }
        public PersonResponseModel(string _name)
        {
            Name = _name;
        }
        public PersonResponseModel(string _name, double _amount)
        {
            Name = _name;
            Amount = _amount;
        }
        public PersonResponseModel(string _name, double _amount, bool _isAdmin, bool _isShared, double _diff, double _fixed, double _flex, double _shared)
        {
            IsShared = _isShared;
            Name = _name;
            Amount = _amount;
            IsAdmin = _isAdmin;
            Diff = _diff;
            Fixed = _fixed;
            Flex = _flex;
            Shared = _shared;

        }
        //public string ReportId { get; set; }
        //public string ExpenseId { get; set; }
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
