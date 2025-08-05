using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Categories.DTOs;

namespace ProductCategoryApp.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;
}
