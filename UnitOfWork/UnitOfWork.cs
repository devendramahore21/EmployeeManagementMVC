using EmployeeManagementMVC.Data;
using EmployeeManagementMVC.Models;
using EmployeeManagementMVC.Repositories;

namespace EmployeeManagementMVC.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Employee> Employees { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new GenericRepository<Employee>(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
