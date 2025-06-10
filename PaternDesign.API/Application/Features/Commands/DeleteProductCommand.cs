using MediatR;
using PaternDesign.API.Domain.Common;

namespace PaternDesign.API.Application.Features.Commands
{
    public record DeleteProductCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
