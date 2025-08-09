using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryApp.Application.Categories.Commands.CreateCategory;
using ProductCategoryApp.Application.Categories.Commands.DeleteCategory;
using ProductCategoryApp.Application.Categories.Commands.UpdateCategory;
using ProductCategoryApp.Application.Categories.DTOs;
using ProductCategoryApp.Application.Categories.Queries.GetCategories;
using ProductCategoryApp.Application.Categories.Queries.GetCategoryById;
using ProductCategoryApp.Web.Models.Requests;

namespace ProductCategoryApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(categories);
        }

        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var command = new CreateCategoryCommand(request.Name);
            var category = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.Id },
                category);
        }

       
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request)
        {
            try
            {
                var command = new UpdateCategoryCommand(id, request.Name);
                var category = await _mediator.Send(command);
                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCategoryCommand(id));
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
