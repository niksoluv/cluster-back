using System.Linq.Expressions;

namespace cluster_back.Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<bool> Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
