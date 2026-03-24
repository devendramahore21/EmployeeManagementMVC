using AutoMapper;
using EmployeeManagementMVC.DTOs;
using EmployeeManagementMVC.Models;

namespace EmployeeManagementMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
