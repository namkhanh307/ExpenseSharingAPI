using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.CalculateModel;
using Repositories.ResponseModel.ExpenseModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private readonly ICalculateService _calculateService;
        public CalculateController(ICalculateService calculateService)
        {
            _calculateService = calculateService;
        }
        [HttpPost("shortTerm")]
        public async Task<IActionResult> CalculateShortTerm([FromBody]CalculatingModel model)
        {
            var result = await _calculateService.CalculateShortTerm(model);
            return Ok(new BaseResponseModel<List<ResponseShortTermModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost("longTerm")]
        public async Task<IActionResult> CalculateLongTerm(string reportId)
        {
            var result = await _calculateService.CalculateLongTerm(reportId);
            return Ok(new BaseResponseModel<ResponseLongTermModel>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
    }
}
