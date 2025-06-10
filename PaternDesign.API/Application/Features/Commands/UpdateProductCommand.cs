using MediatR;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Commands
{
    public record UpdateProductCommand : IRequest<Result<string>>
    {
        public UpdateProductDTO Product { get; set; }

        public UpdateProductCommand(UpdateProductDTO product)
        {
            Product = product;
        }
    }
}
