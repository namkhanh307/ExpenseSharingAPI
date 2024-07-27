using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class ResponseShortTermModel
    {
        public ResponseShortTermModel(string _personPay, string _personReceive, double _amount)
        {
            PersonPay = _personPay;
            PersonReceive = _personReceive;
            Amount = _amount;
        }
        //public string PersonPayId { get; set; }
        //public string PersonReceiveId { get; set; }
        public string PersonPay {  get; set; } = string.Empty;
        public string PersonReceive { get; set; } = string.Empty;
        public double Amount { get; set; } = double.MinValue;
    }
}
