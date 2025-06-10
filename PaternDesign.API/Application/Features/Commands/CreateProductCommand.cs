using MediatR;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Commands
{
    public record CreateProductCommand : IRequest<Result<string>>
    {
        public CreateProductDTO Product { get; set; }

        public CreateProductCommand(CreateProductDTO product)
        {
            Product = product;
        }
    }
}
