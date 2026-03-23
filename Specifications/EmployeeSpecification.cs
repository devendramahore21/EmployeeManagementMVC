using EmployeeManagementMVC.Models;

namespace EmployeeManagementMVC.Specifications
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification(string? search, string? sortOrder, int page, int pageSize)
        : base(e => string.IsNullOrEmpty(search) || e.Name.Contains(search))
        {
            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    ApplyOrderByDescending(q => q.OrderByDescending(e => e.Name));
                    break;

                case "Email":
                    ApplyOrderBy(q => q.OrderBy(e => e.Email));
                    break;

                case "email_desc":
                    ApplyOrderByDescending(q => q.OrderByDescending(e => e.Email));
                    break;

                case "Salary":
                    ApplyOrderBy(q => q.OrderBy(e => e.Salary));
                    break;

                case "salary_desc":
                    ApplyOrderByDescending(q => q.OrderByDescending(e => e.Salary));
                    break;

                default:
                    ApplyOrderBy(q => q.OrderBy(e => e.Name));
                    break;
            }

            // ✅ Server-side pagination
            ApplyPaging((page - 1) * pageSize, pageSize);
        }
    }
}
