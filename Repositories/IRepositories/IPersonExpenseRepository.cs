
using Repositories.Entities;

namespace Repositories.IRepositories
{
    public interface IPersonExpenseRepository
    {
        Task DeletePersonExpense(string personId, string expenseId);
        Task UpdatePersonExpense(PersonExpense personExpense);

    }
}
