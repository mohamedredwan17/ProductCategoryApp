using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryApp.Application.Products.Commands.CreateProduct;
using ProductCategoryApp.Application.Products.Commands.DeleteProduct;
using ProductCategoryApp.Application.Products.Commands.UpdateProduct;
using ProductCategoryApp.Application.Products.DTOs;
using ProductCategoryApp.Application.Products.Queries.GetProductById;
using ProductCategoryApp.Application.Products.Queries.GetProducts;
using ProductCategoryApp.Web.Models.Requests;

namespace ProductCategoryApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto request)
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Price,
                request.Currency,
                request.CategoryId
            );

            var product = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.Id },
                product);
        }

        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] CreateProductRequest request)
        {
            try
            {
                var command = new UpdateProductCommand(
                    id,
                    request.Name,
                    request.Price,
                    request.Currency,
                    request.CategoryId
                );

                var product = await _mediator.Send(command);
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

       
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
