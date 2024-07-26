using Repositories.Entities;
using Repositories.ResponseModel.ExpenseModel;

namespace Services.IServices
{
    public interface IExpenseService
    {
        void DeleteExpense(string id);
        List<GetExpenseModel> GetExpenses(string? reportId, string? type, DateTime? fromdate, DateTime? endDate, string? expenseName);
        Task PostExpense(PostExpenseModel model);
        Task PutExpense(string id, PutExpenseModel model);
    }
}
