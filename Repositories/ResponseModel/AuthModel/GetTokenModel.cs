using Repositories.ResponseModel.PersonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.AuthModel
{
    public class GetTokenModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
