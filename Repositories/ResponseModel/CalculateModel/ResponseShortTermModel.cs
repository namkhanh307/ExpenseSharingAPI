using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class ResponseShortTermModel
    {
        public string PersonPay {  get; set; } = string.Empty;
        public double Amount { get; set; } = double.MinValue;
        public string PersonReceive { get; set; } = string.Empty;
    }
}
