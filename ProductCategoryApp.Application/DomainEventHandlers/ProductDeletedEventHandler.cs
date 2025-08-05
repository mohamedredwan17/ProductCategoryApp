using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCategoryApp.Application.Common;
using ProductCategoryApp.Domain.Categories;
using ProductCategoryApp.Domain.Products.Events;

namespace ProductCategoryApp.Application.DomainEventHandlers
{
    public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductDeletedEventHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(notification.CategoryId, cancellationToken);
            if (category != null)
            {
                category.DecrementProductCount();
                _categoryRepository.Update(category);
            }
        }
    }
}
