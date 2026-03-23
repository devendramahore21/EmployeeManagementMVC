using System.Linq.Expressions;

namespace EmployeeManagementMVC.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }

        Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get; }

        int Skip { get; }

        int Take { get; }

        bool IsPagingEnabled { get; }
    }
}
