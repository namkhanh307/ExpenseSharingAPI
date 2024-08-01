using Repositories.Entities;
using Repositories.ResponseModel.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ITokenService
    {
        Task<GetTokenModel> GenerateTokens(Person person);

    }
}
