using cluster_back.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationContext _context { get; set; }
        protected ILogger _logger { get; set; }
        public RepositoryBase(ApplicationContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public IQueryable<T> FindAll()
        {
            try
            {
                return _context.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindAll method error", typeof(T));
                return null;
            }
        }
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _context.Set<T>().Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindByCondition method error", typeof(T));
                return null;
            }
        }
        public virtual async Task<bool> Create(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Create method error", typeof(T));
                return false;
            }
        }
        public virtual bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(T));
                return false;
            }
        }
        public virtual bool Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(T));
                return false;
            }
        }
    }
}
