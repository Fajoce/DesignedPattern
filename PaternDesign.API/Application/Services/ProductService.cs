using AutoMapper;
using MediatR;
using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Application.Handlers;
using PaternDesign.API.Domain.Abstractions;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Services
{
    public class ProductService
    {
        private readonly IRepository<Products> _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;  // <-- Inyectamos MediatR
      

        public ProductService(IRepository<Products> repo, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this.   _mediator = mediator;
         
        }
        #region EUEM
        #region DaC
        //minimize complexity
        //Comply with language conventions
        public async Task<Result<IEnumerable<ProductDTO>>> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            var spec = new ProductPriceSpecification(minPrice, maxPrice);
            var products = await _repo.ObtenerPorPrecio(spec);

            if (products == null || !products.Any())
            {
                return Result<IEnumerable<ProductDTO>>.FailureResult("No products found.");
            }
            #region DRY
            //return ValidateAndMapProducts(products);
            #endregion
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Result<IEnumerable<ProductDTO>>.SuccessResult(productDTOs);
        }
        #endregion DaC
        #endregion EUEM
        public async Task<Result<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            // Obtener los productos desde el repositorio
            var products = await _repo.ObtenerTodosAsync();

            // Verificar si los productos fueron encontrados
            if (products == null || !products.Any())
            {
                // Retornar un Result con error si no hay productos
                return Result<IEnumerable<ProductDTO>>.FailureResult("No products found.");
            }

            // Mapear los productos a ProductDTO
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            // Retornar un Result exitoso con los datos
            return Result<IEnumerable<ProductDTO>>.SuccessResult(productDTOs);
            #region DRY
            //return ValidateAndMapProducts(products);
            #endregion
        }
        public async Task<Result<ProductDTO>> GetProductById(int id)
        {
            var product = await _repo.ObtenerPorIdAsync(id);
            if (product == null)
            {
                return Result<ProductDTO>.FailureResult("Product not found.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.SuccessResult(productDTO);
        }
        public async Task<Result<string>> CreateProduct(CreateProductDTO dto)
        {
            var product = _mapper.Map<Products>(dto);
            await _repo.AgregarAsync(product);          
           
            // Agregar el evento de dominio (IDomainEvent)
            product.AddDomainEvent(new ProductCreatedDomainEvent(product));

            await _unitOfWork.SaveChangesAsync(); // Confirmamos la transacción

            // Publicar el evento utilizando MediatR (INotification)
            await _mediator.Publish(new ProductCreatedEvent(product));

            return Result<string>.SuccessResult("Product created successfully.");
        }

        public async Task<Result<string>> UpdateProduct(UpdateProductDTO dto)
        {
            var product = await _repo.ObtenerPorIdAsync(dto.ProductId);
            if (product == null)
            {
                return Result<string>.FailureResult("Product not found for update.");
            }

            _mapper.Map(dto, product);
            await _repo.ActualizarAsync(product);
            await _unitOfWork.SaveChangesAsync(); // Confirmamos la transacción
            return Result<string>.SuccessResult("Product updated successfully.");
        }

        public async Task<Result<string>> DeleteProduct(int id)
        {
            var product = await _repo.ObtenerPorIdAsync(id);
            if (product == null)
            {
                return Result<string>.FailureResult("Product not found for deletion.");
            }

            await _repo.EliminarAsync(id);
            await _unitOfWork.SaveChangesAsync(); // Confirmamos la transacción
            return Result<string>.SuccessResult("Product deleted successfully.");
        }

        #region Private methods
        private Result<IEnumerable<ProductDTO>> ValidateAndMapProducts(IEnumerable<Products> products)
        {
            if (products == null || !products.Any())
                return Result<IEnumerable<ProductDTO>>.FailureResult("No products found.");

            var dtos = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Result<IEnumerable<ProductDTO>>.SuccessResult(dtos);
        }
        #endregion
    }
}
