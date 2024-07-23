using Repositories.ResponseModel.PersonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.AuthModel
{
    public class GetLogInModel
    {
        public GetPersonModel? Person { get; set; }
        public GetTokenModel? Token { get; set; }
    }
}
