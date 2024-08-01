
namespace Repositories.IRepositories
{
    public interface IPersonExpenseRepository
    {
        Task DeletePersonExpense(string personId, string expenseId);
    }
}
