using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonExpensesController : ControllerBase
    {
        private readonly IPersonExpenseService _personExpenseService;
        public PersonExpensesController(IPersonExpenseService personExpenseService)
        {
            _personExpenseService = personExpenseService;
        }
        [HttpGet]
        public List<GetPersonExpenseModel> GetPersonExpenses()
        {
            return _personExpenseService.GetPersonExpenses();
        }
        [HttpPost]
        public void PostPersonExpense(PostPersonExpenseModel model)
        {
            _personExpenseService.PostPersonExpense(model);
        }
        [HttpPut]
        public void PutPersonExpense(string id, PutPersonExpenseModel model)
        {
            _personExpenseService.PutPersonExpense(id, model);
        }
        [HttpDelete]
        public void DeletePersonExpense(string id)
        {
            _personExpenseService.DeletePersonExpense(id);
        }
    }
}
