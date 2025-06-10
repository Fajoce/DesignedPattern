using AutoMapper;
using MediatR;
using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Queries
{
    public class GetProductsByPriceQueryHandler : IRequestHandler<GetProductsByPriceQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;
        public GetProductsByPriceQueryHandler(IRepository<Products> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsByPriceQuery request, CancellationToken cancellationToken)
        {
            // Crear la especificación para el filtro de precio
            var spec = new ProductPriceSpecification(request.MinPrice, request.MaxPrice);

            // Obtener los productos de acuerdo con la especificación
            var products = await _repo.ObtenerPorPrecio(spec);

            // Verificar si los productos no se encuentran
            if (products == null || !products.Any())
            {
                return Result<IEnumerable<ProductDTO>>.FailureResult("No products found.");
            }

            // Mapear los productos a DTOs
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            // Retornar los productos en un resultado exitoso
            return Result<IEnumerable<ProductDTO>>.SuccessResult(productDTOs);
        }
    }    
}
