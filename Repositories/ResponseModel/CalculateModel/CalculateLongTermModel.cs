using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class CalculateLongTermModel
    {
        public required string GroupId { get; set; }
        public required string ReportId { get; set; }
    }
}
