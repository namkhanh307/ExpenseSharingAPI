using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.ExpenseModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IExpenseService expenseService) : ControllerBase
    {
        private readonly IExpenseService _expenseService = expenseService;

        [HttpGet]
        public async Task<IActionResult> GetExpenses(string? reportId, string? type, DateTime? fromDate, DateTime? endDate, string? expenseName)
        {
            List<GetExpenseModel?> result = await _expenseService.GetExpenses(reportId, type, fromDate, endDate, expenseName);
            return Ok(new BaseResponseModel<List<GetExpenseModel?>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostExpense(PostExpenseModel model)
        {
            await _expenseService.PostExpense(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Thêm chi tiêu thành công"));
        }
        [HttpPut]
        public async Task<IActionResult> PutExpense(PutExpenseModel model)
        {
            await _expenseService.PutExpense(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chỉnh sửa chi tiêu thành công"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            await _expenseService.DeleteExpense(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xóa chi tiêu thành công"));
        }
    }
}
