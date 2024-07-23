using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class PersonShortTermModel
    {
        public required string Name { get; set; }
        public required double Amount { get; set; }
    }
}
