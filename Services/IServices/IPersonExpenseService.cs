using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;

namespace Services.IServices
{
    public interface IPersonExpenseService
    {
        Task DeletePersonExpense(string expenseId, string personId);
        Task<GetPersonExpenseModel> GetPersonExpenses(string? reportId, string? expenseId);
        Task PostPersonExpense(PostPersonExpenseModel model);
        Task PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model);
        Task PutPersonExpense(string expenseId, PutPersonExpenseModel model);
    }
}
