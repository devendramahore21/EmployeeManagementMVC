using EmployeeManagementMVC.Data;
using EmployeeManagementMVC.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVC.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
                _context = context;
                _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
             _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        //public async Task<List<T>> GetWithSpecificationAsync(ISpecification<T> spec)
        public async Task<(List<T> Data, int TotalCount)> GetWithSpecificationAsync(ISpecification<T> spec)
        {
            var query = _dbSet.AsQueryable();

            // Filter
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            // Count BEFORE paging
            var totalCount = await query.CountAsync();

            // Sorting
            if (spec.OrderBy != null)
                query = spec.OrderBy(query);

            if (spec.OrderByDescending != null)
                query = spec.OrderByDescending(query);

            // Paging
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            var data = await query.ToListAsync();

            return (data, totalCount);
        }
    }
}
