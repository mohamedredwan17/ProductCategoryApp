using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Products.Entities;

namespace ProductCategoryApp.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        void Update(Product product);
        void Delete(Product product);
    }
}
