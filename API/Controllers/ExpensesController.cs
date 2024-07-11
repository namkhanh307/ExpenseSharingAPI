using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
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
        public List<GetExpenseModel> GetExpenses()
        {
            return _expenseService.GetExpenses();
        }
        [HttpPost]
        public void PostExpense(PostExpenseModel model)
        {
            _expenseService.PostExpense(model);
        }
        [HttpPut]
        public void PutExpense(string id, PutExpenseModel model)
        {
            _expenseService.PutExpense(id, model);
        }
        [HttpDelete]
        public void DeleteExpense(string id)
        {
            _expenseService.DeleteExpense(id);
        }
    }
}
