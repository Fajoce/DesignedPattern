using MediatR;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Queries
{
    public class GetProductByIdQuery : IRequest<Result<ProductDTO>>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
