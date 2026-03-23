using EmployeeManagementMVC.Models;
using EmployeeManagementMVC.Repositories;

namespace EmployeeManagementMVC.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }

        Task SaveAsync();
    }
}
