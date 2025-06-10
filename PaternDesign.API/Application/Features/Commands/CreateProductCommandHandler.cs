using AutoMapper;
using MediatR;
using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;
        private readonly IPricingStrategyFactory _pricingStrategy;

        public CreateProductCommandHandler(IRepository<Products> repo, IMapper mapper, IPricingStrategyFactory pricingStrategy)
        {
            _repo = repo;
            _mapper = mapper;
            _pricingStrategy = pricingStrategy;
        }

        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Products>(request.Product);
            // Aplicar estrategia de precio
            var finalPrice = _pricingStrategy.CalculateFinalPrice(product);
            product.ProductPrice = finalPrice;
            await _repo.AgregarAsync(product);
            return Result<string>.SuccessResult("Product created successfully." + finalPrice);
        }
    }
}
