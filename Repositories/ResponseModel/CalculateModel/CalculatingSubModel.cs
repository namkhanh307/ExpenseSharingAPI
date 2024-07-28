using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculatingSubModel
    {
        public List<PersonCalculatingModel> PersonCalculatingSubModel { get; set; }
        public string ExpenseId { get; set; }
        public string ReportId { get; set; }
    }
}
