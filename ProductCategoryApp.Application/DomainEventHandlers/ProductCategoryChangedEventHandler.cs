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
    public class ProductCategoryChangedEventHandler : INotificationHandler<ProductCategoryChangedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryChangedEventHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ProductCategoryChangedEvent notification, CancellationToken cancellationToken)
        {
            // Decrement count from old category
            var oldCategory = await _categoryRepository.GetByIdAsync(notification.OldCategoryId, cancellationToken);
            if (oldCategory != null)
            {
                oldCategory.DecrementProductCount();
                _categoryRepository.Update(oldCategory);
            }

            // Increment count in new category
            var newCategory = await _categoryRepository.GetByIdAsync(notification.NewCategoryId, cancellationToken);
            if (newCategory != null)
            {
                newCategory.IncrementProductCount();
                _categoryRepository.Update(newCategory);
            }
        }
    }
}
