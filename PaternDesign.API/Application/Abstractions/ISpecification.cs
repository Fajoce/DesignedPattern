using System.Linq.Expressions;

namespace PaternDesign.API.Application.Abstractions
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderBy { get; }
        List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
    }
}
