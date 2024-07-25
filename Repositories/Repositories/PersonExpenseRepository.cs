using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;

namespace Repositories.Repositories
{
    public class PersonExpenseRepository : IPersonExpenseRepository
    {
        private readonly ExpenseSharingContext _context;

        public PersonExpenseRepository(ExpenseSharingContext context) : base()
        {
            _context = context;
        }
        public void DeletePersonExpense(string personId, string expenseId)
        {
            var result = _context.PersonExpenses.Where(pe => pe.ExpenseId == expenseId && pe.PersonId == personId).FirstOrDefault();    
            if (result != null)
            {
                _context.Remove(result);
            }
        }
    }
}
