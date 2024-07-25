using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;

namespace Services.IServices
{
    public interface IPersonExpenseService
    {
        void DeletePersonExpense(string expenseId, string personId);
        List<GetPersonExpenseModel> GetPersonExpenses(string? reportId, string? expenseId);
        void PostPersonExpense(PostPersonExpenseModel model);
        void PutPersonExpense(string expenseId, PutPersonExpenseModel model);
    }
}
