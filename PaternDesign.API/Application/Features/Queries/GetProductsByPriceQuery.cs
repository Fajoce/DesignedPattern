using MediatR;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Queries
{
    public class GetProductsByPriceQuery: IRequest<Result<IEnumerable<ProductDTO>>>
{
        public decimal MinPrice { get; }
        public decimal MaxPrice { get; }
    

        public GetProductsByPriceQuery(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
         
        }
}
}
