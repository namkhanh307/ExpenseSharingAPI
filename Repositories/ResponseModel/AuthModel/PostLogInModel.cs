using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.AuthModel
{
    public class PostLogInModel
    {
        public required string Phone { get; set; }
        public required string Password { get; set; }
    }
}
