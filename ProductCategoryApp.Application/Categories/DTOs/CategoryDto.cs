using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Application.Categories.DTOs
{
    public record CategoryDto(
    Guid Id,
    string Name,
    int ProductCount
);
}
