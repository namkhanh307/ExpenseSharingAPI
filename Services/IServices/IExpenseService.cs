using Repositories.Entities;
using Repositories.ResponseModel.ExpenseModel;

namespace Services.IServices
{
    public interface IExpenseService
    {
        Task DeleteExpense(string id);
        Task<List<GetExpenseModel?>> GetExpenses(string? reportId, string? type, DateTime? fromdate, DateTime? endDate, string? expenseName);
        Task PostExpense(PostExpenseModel model);
        Task PutExpense(PutExpenseModel model);
    }
}
