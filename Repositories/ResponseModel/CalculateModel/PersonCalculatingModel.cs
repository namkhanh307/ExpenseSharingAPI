using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class PersonCalculatingModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public double Amount { get; set; }
        public bool IsShared { get; set; } = true;
    }
}
