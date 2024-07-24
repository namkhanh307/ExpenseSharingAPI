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
        public PersonResponseModel(string _name, double _amount, bool _isAdmin, double _diff, double _fixed, double _flex, double _shared)
        {
            Name = _name;
            Amount = _amount;
            IsAdmin = _isAdmin;
            Diff = _diff;
            Fixed = _fixed;
            Flex = _flex;
            Shared = _shared;
            
        }
        public string Name { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public double Amount { get; set; }
        public double Diff{ get; set; }
        public double Fixed { get; set; }
        public double Flex { get; set; }
        public double Shared { get; set; }
    }
}
