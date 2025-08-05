
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Categories.DTOs;

namespace ProductCategoryApp.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
