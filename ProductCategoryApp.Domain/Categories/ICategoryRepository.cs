using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Categories.Entities;

namespace ProductCategoryApp.Domain.Categories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        void Update(Category category);
        void Delete(Category category);
    }
}
