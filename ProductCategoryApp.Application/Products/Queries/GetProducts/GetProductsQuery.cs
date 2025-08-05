using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Products.DTOs;

namespace ProductCategoryApp.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
