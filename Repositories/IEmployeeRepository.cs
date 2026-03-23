using EmployeeManagementMVC.Models;

namespace EmployeeManagementMVC.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();

        Task<Employee?> GetByIdAsync(int id);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}
