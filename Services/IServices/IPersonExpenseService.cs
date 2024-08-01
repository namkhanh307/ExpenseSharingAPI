using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;

namespace Services.IServices
{
    public interface IPersonExpenseService
    {
        Task<List<GetPersonExpenseModel>> GetPersonExpenses(string? reportId, string? expenseId);
        Task PostPersonExpense(PostPersonExpenseModel model);
        Task PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model);
        Task PutPersonExpense(string expenseId, PutPersonExpenseModel model);
        Task DeletePersonExpense(string expenseId, string personId);
    }
}
