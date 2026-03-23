using EmployeeManagementMVC.DTOs;

namespace EmployeeManagementMVC.Services
{
    public interface IGenericService<TDto>
    {
        IQueryable<TDto> GetAll();

        Task<TDto?> GetByIdAsync(int id);

        Task AddAsync(TDto dto);

        Task UpdateAsync(TDto dto);

        Task DeleteAsync(int id);
        
    }
}
