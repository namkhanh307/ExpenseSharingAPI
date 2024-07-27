using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculatingModel
    {
        public List<PersonCalculatingModel> PersonCalculatingModel { get; set; }
        public List<CalculatingSubModel> CalculatingSubModel { get; set; }
    }
}
