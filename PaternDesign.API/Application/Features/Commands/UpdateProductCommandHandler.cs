using AutoMapper;
using MediatR;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepository<Products> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.ObtenerPorIdAsync(request.Product.ProductId);
            if (product == null)
            {
                return Result<string>.FailureResult("Product not found for update.");
            }

            _mapper.Map(request.Product, product);
            await _repo.ActualizarAsync(product);
            return Result<string>.SuccessResult("Product updated successfully.");
        }
    }
}
