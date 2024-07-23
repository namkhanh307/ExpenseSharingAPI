using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculateShortTermModel
    {
        public required List<string> Persons { get; set; }
        public required List<CalculatedPersonModel> CalculatedPersonModels { get; set; }
    }
}
