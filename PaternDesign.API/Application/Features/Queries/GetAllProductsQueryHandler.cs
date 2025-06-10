using AutoMapper;
using MediatR;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Products> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.ObtenerTodosAsync();
            if (products == null || !products.Any())
            {
                return Result<IEnumerable<ProductDTO>>.FailureResult("No products found.");
            }

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Result<IEnumerable<ProductDTO>>.SuccessResult(productDTOs);
        }
    }
}
