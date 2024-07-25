using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;

namespace Repositories.Repositories
{
    public class PersonGroupRepository : IPersonGroupRepository
    {
        private readonly ExpenseSharingContext _context;
        public PersonGroupRepository(ExpenseSharingContext context) : base()
        {
            _context = context;
        }
    }
}
