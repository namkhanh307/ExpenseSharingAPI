
namespace Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonGroupRepository PersonGroupRepository { get; }
        IPersonExpenseRepository PersonExpenseRepository { get; }
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
