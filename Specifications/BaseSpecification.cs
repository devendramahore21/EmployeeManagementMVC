using System.Linq.Expressions;

namespace EmployeeManagementMVC.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; private set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            OrderBy = orderBy;
        }
        protected void ApplyOrderByDescending(Func<IQueryable<T>, IOrderedQueryable<T>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }
    }
}
