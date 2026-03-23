using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagementMVC.DTOs;
using EmployeeManagementMVC.Models;
using EmployeeManagementMVC.Repositories;
using EmployeeManagementMVC.Specifications;
using EmployeeManagementMVC.UnitOfWork;
using EmployeeManagementMVC.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;

namespace EmployeeManagementMVC.Services
{
    public class EmployeeService : GenericService<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeService(
             //IGenericRepository<Employee> repository,
             IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork.Employees, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(EmployeeDTO dto)
        {
            var employee = _mapper.Map<Employee>(dto);

            await _unitOfWork.Employees.AddAsync(employee);

            await _unitOfWork.SaveAsync();
        }

        //public async Task<IPagedList<EmployeeDTO>> GetEmployeesAsync(
        //    string search,
        //    string sortOrder,
        //    int page,
        //    int pageSize)
        public async Task<PagedResult<EmployeeDTO>> GetEmployeesAsync(
            string? search,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var spec = new EmployeeSpecification(search, sortOrder, page, pageSize);

            var result = await _unitOfWork
                            .Employees
                            .GetWithSpecificationAsync(spec);

            return new PagedResult<EmployeeDTO>
            {
                Items = _mapper.Map<List<EmployeeDTO>>(result.Data),
                TotalCount = result.TotalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }
    }
}
