using Repositories.ResponseModel.CalculateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICalculateService
    {
        Task<ResponseLongTermModel> CalculateLongTerm(string reportId);
        Task<List<ResponseShortTermModel>> CalculateShortTerm(CalculatingModel model);
    }
}
