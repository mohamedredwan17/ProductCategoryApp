using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Products.DTOs;

namespace ProductCategoryApp.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId
) : IRequest<ProductDto>;
}
