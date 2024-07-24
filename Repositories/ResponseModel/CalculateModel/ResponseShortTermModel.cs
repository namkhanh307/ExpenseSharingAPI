using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.CalculateModel
{
    public class ResponseShortTermModel
    {
        public ResponseShortTermModel(PersonResponseModel _personPay, PersonResponseModel _personReceive, double _amount)
        {
            PersonPay = _personPay;
            PersonReceive = _personReceive;
            Amount = _amount;
        }
        public PersonResponseModel PersonPay {  get; set; } = new PersonResponseModel();
        public double Amount { get; set; } = double.MinValue;
        public PersonResponseModel PersonReceive { get; set; } = new PersonResponseModel();
    }
}
