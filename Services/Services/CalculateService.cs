using Repositories.ResponseModel.CalculateModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CalculateService : ICalculateService
    {
        public List<ResponseShortTermModel>? CalculateShortTerm(List<CalculateShortTermModel> model)
        {
            Console.WriteLine(JsonSerializer.Serialize(model));        
            return null;
        }
    }
}
