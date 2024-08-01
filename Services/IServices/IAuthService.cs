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
        Task<GetPersonModel> GetInfo();
        Task<GetLogInModel> LogIn(PostLogInModel model);
        Task SignUp(PostSignUpModel model);
    }
}
