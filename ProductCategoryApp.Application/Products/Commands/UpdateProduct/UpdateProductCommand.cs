using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Products.DTOs;

namespace ProductCategoryApp.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId
) : IRequest<ProductDto>;
}
