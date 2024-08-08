using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.PersonExpenseModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonExpensesController(IPersonExpenseService personExpenseService) : ControllerBase
    {
        private readonly IPersonExpenseService _personExpenseService = personExpenseService;

        [HttpGet]
        public async Task<IActionResult> GetPersonExpenses(string reportId, string? expenseId)
        {
            var result = await _personExpenseService.GetPersonExpenses(reportId, expenseId);
            return Ok(new BaseResponseModel<GetPersonExpenseModel>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost("ForApp")]
        public async Task<IActionResult> PostPersonExpense(PostPersonExpenseModel model)
        {
            await _personExpenseService.PostPersonExpense(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tạo mới chi tiêu cho thành viên thành công"));
        }
        [HttpPost("forDeveloping")]
        public async Task<IActionResult> PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model)
        {
            await _personExpenseService.PostPersonExpenseForDeveloping(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tạo mới chi tiêu cho thành viên thành công"));
        }
        [HttpPut("forApp")]
        public async Task<IActionResult> PutPersonExpenseForApp(string expenseId, PutPersonExpenseModel model)
        {
            await _personExpenseService.PutPersonExpense(expenseId, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chỉnh sửa chi tiêu cho thành viên thành công"));
        }
        [HttpPut("forDeveloping")]
        public async Task<IActionResult> PutPersonExpenseForDeveloping(string expenseId, PutPersonExpenseModel model)
        {
            await _personExpenseService.PutPersonExpenseForDeveloping(expenseId, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chỉnh sửa chi tiêu cho thành viên thành công"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonExpense(string expenseId, string personId)
        {
            await _personExpenseService.DeletePersonExpense(expenseId, personId);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xóa chi tiêu cho thành viên thành công"));
        }
    }
}
