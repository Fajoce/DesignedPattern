using MediatR;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Queries
{
    public record GetAllProductsQuery: IRequest<Result<IEnumerable<ProductDTO>>>
    {
    }
}
