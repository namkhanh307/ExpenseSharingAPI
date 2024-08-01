using System.Linq.Expressions;

namespace Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Detach(T entity);
        IEnumerable<T> Get(int index, int pageSize);
        IQueryable<T> Entities { get; }
        T GetById(object id);
        void Insert(T obj);
        void InsertRange(List<T> obj);
        Task InsertCollection(ICollection<T> collection);
        void Update(T obj);
        void Delete(object id);
        void Save();
        Task<IEnumerable<T>> GetAsync(int index, int pageSize);
        Task<T> GetByIdAsync(object id);
        List<T> GetAllAsync();
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task UpdateRangeAsync(List<T> obj); 
        Task DeleteAsync(object id);
        Task SaveAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllQueryableAsync();
        Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate);

    }
}
