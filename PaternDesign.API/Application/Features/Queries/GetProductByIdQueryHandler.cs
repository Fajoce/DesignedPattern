using AutoMapper;
using MediatR;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Features.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepository<Products> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repo.ObtenerPorIdAsync(request.Id);
            if (product == null)
            {
                return Result<ProductDTO>.FailureResult("Product not found.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.SuccessResult(productDTO);
        }
    }
}
