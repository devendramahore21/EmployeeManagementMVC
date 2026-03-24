using EmployeeManagementMVC.DTOs;
using EmployeeManagementMVC.ViewModels;

namespace EmployeeManagementMVC.Services
{
    public interface IEmployeeService : IGenericService<EmployeeDTO>
    {
        //Task<IPagedList<EmployeeDTO>> GetEmployeesAsync(
        //string search,
        //string sortOrder,
        //int page,
        //int pageSize);
        Task<PagedResult<EmployeeDTO>> GetEmployeesAsync(
            string? search,
            string? sortOrder,
            int page,
            int pageSize);
    }
}
