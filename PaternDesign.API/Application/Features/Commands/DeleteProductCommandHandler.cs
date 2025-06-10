using MediatR;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<string>>
    {
        private readonly IRepository<Products> _repo;

        public DeleteProductCommandHandler(IRepository<Products> repo)
        {
            _repo = repo;
        }

        public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.ObtenerPorIdAsync(request.Id);
            if (product == null)
            {
                return Result<string>.FailureResult("Product not found for deletion.");
            }

            await _repo.EliminarAsync(request.Id);
            return Result<string>.SuccessResult("Product deleted successfully.");
        }
    }
}
