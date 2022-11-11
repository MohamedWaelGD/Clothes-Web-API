using Clothes_Web_API_App.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clothes_Web_API_App.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<T> FindAll()
        {
            return _appDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task<T?> FindFirstByCondition(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
    }
}
