using PaternDesign.API.Domain.Entities;
using System.Linq.Expressions;

namespace PaternDesign.API.Application.Abstractions
{
    public class ProductPriceSpecification : ISpecification<Products>
    {
        public ProductPriceSpecification(decimal minPrice, decimal maxPrice)
        {
            Criteria = p => p.ProductPrice >= minPrice && p.ProductPrice <= maxPrice;
        }

        public Expression<Func<Products, bool>> Criteria { get; }
        public List<Expression<Func<Products, object>>> Includes { get; } = new List<Expression<Func<Products, object>>>();
        public List<Func<IQueryable<Products>, IOrderedQueryable<Products>>> OrderBy { get; } = new List<Func<IQueryable<Products>, IOrderedQueryable<Products>>>();
        public List<Func<IQueryable<Products>, IOrderedQueryable<Products>>> OrderByDescending { get; } = new List<Func<IQueryable<Products>, IOrderedQueryable<Products>>>();
        public int Take { get; } = 0;
        public int Skip { get; } = 0;
    }
}
