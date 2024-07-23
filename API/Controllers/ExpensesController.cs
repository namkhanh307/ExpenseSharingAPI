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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult GetExpenses(string? reportId, string? type)
        {
            var result = _expenseService.GetExpenses(reportId, type);
            return Ok(new BaseResponseModel<List<GetExpenseModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public IActionResult PostExpense(PostExpenseModel model)
        {
            _expenseService.PostExpense(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tao chi tieu thanh cong"));
        }
        [HttpPut]
        public IActionResult PutExpense(string id, PutExpenseModel model)
        {
            _expenseService.PutExpense(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua chi tieu thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeleteExpense(string id)
        {
            _expenseService.DeleteExpense(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa chi tieu thanh cong"));
        }
    }
}
