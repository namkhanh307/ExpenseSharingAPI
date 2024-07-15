using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.PersonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthService
    {
        GetPersonModel GetInfo();
        GetLogInModel LogIn(PostLogInModel model);
        void SignUp(PostSignUpModel model);
    }
}
