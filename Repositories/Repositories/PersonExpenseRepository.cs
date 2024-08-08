using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;

namespace Repositories.Repositories
{
    public class PersonExpenseRepository(ExpenseSharingContext context) : IPersonExpenseRepository
    {
        private readonly ExpenseSharingContext _context = context;

        public async Task DeletePersonExpense(string personId, string expenseId)
        {
            PersonExpense? result = await _context.PersonExpenses.Where(pe => pe.ExpenseId == expenseId && pe.PersonId == personId).FirstOrDefaultAsync();    
            if (result != null)
            {
                _context.Remove(result);
            }
        }
        public async Task UpdatePersonExpense(PersonExpense personExpense)
        {
            _context.PersonExpenses.Update(personExpense);
            await _context.SaveChangesAsync();
        }

    }
}
