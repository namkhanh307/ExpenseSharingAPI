using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class ResponseLongTermModel
    {
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public List<ResponseShortTermModel>? ResponseShortTerm { get; set; }
    }
}
