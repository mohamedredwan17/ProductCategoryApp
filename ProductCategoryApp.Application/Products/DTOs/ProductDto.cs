using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Application.Products.DTOs
{
    public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId
);
}
