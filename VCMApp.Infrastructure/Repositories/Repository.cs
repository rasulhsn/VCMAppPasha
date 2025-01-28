using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VCMApp.Application.Contracts;
using VCMApp.Infrastructure.Persistence;

namespace VCMApp.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly VCMDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(VCMDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
