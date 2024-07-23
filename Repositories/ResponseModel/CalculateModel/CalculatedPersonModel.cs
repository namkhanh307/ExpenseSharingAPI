using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculatedPersonModel
    {
        public required List<string> SubPersons { get; set; }
        public required List<PersonShortTermModel> PersonShortTerms { get; set; }
    }
}
