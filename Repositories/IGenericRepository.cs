using EmployeeManagementMVC.Specifications;

namespace EmployeeManagementMVC.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
        //Task<List<T>> GetWithSpecificationAsync(ISpecification<T> spec);
        Task<(List<T> Data, int TotalCount)> GetWithSpecificationAsync(ISpecification<T> spec);

        Task SaveAsync();
    }
}
