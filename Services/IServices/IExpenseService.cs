using Repositories.Entities;
using Repositories.ResponseModel.ExpenseModel;

namespace Services.IServices
{
    public interface IExpenseService
    {
        void DeleteExpense(string id);
        List<GetExpenseModel> GetExpenses(string? reportId, string? type);
        void PostExpense(PostExpenseModel model);
        void PutExpense(string id, PutExpenseModel model);
    }
}
