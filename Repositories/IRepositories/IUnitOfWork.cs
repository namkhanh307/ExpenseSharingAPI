
namespace Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGroupRepository GroupRepository { get; }
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
