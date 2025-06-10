using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaternDesign.API.Application.Features.Commands;
using PaternDesign.API.Application.Features.Queries;
using PaternDesign.API.Application.Services;
using PaternDesign.API.Domain.Common;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productoService;
        private readonly ISender _mediator;

        public ProductsController(ProductService productoService, ISender mediator)
        {
            _productoService = productoService;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return !result.Success
                ? NotFound(Result<string>.FailureResult(result.ErrorMessages))
                : Ok(result.Data);
        }
        [HttpGet("price")]
        public async Task<IActionResult> GetProductsByPrice([FromQuery] decimal minPrice, decimal maxPrice)
        {
            // Crear y enviar el query a través de MediatR
            var query = new GetProductsByPriceQuery(minPrice, maxPrice);
            var result = await _mediator.Send(query);

            // Evaluar el resultado
            return !result.Success
                ? NotFound(Result<string>.FailureResult(result.ErrorMessages))  // Si no es exitoso, devuelve NotFound
                : Ok(result.Data);  // Si es exitoso, devuelve el DTO de productos
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return !result.Success
                ? NotFound(Result<string>.FailureResult(result.ErrorMessages))
                : Ok(result.Data);
        }
        /// <summary>
        /// Endpoint HTTP POST para crear un nuevo producto.
        /// </summary>
        /// <param name="dto">Objeto DTO que contiene los datos necesarios para crear el producto.</param>
        /// <returns>
        /// Devuelve un resultado 201 (Created) si la creación fue exitosa, junto con la ubicación del nuevo recurso.
        /// Si ocurre un error, devuelve un resultado 400 (BadRequest) con los mensajes de error.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDTO dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(dto));

            return !result.Success
                ? BadRequest(Result<string>.FailureResult(result.ErrorMessages))
                : CreatedAtAction(nameof(GetProductById), new { id = dto.ProductID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductDTO dto)
        {
            if (id != dto.ProductId) return BadRequest("Product ID mismatch.");
            var result = await _mediator.Send(new UpdateProductCommand(dto));
            return !result.Success
                ? NotFound(Result<string>.FailureResult(result.ErrorMessages))
                : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return !result.Success
                ? NotFound(Result<string>.FailureResult(result.ErrorMessages))
                : NoContent();
        }
    }
}
