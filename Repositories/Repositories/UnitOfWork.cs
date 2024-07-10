using Repositories.Entities;
using Repositories.IRepositories;

namespace Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly ExpenseSharingContext _dbContext;
        public UnitOfWork(ExpenseSharingContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IGroupRepository _groupRepository;
        public IGroupRepository GroupRepository
        {

            get { return _groupRepository ??= new GroupRepository(_dbContext); }
        }
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
