using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Common;
using ProductCategoryApp.Domain.Products.ValueObjects;
using ProductCategoryApp.Domain.Products.Events;

namespace ProductCategoryApp.Domain.Products.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public Price Price { get; private set; } = null!;
        public CategoryId CategoryId { get; private set; } = null!;

        private Product() { } 

        private Product(string name, Price price, CategoryId categoryId)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;

            RaiseDomainEvent(new ProductCreatedEvent(Id, CategoryId));
        }

        public static Product Create(string name, Price price, CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            return new Product(name, price, categoryId);
        }

        public void UpdateDetails(string name, Price price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            Name = name;
            Price = price;
        }

        public void UpdateCategory(CategoryId newCategoryId)
        {
            var oldCategoryId = CategoryId;
            CategoryId = newCategoryId;

            RaiseDomainEvent(new ProductCategoryChangedEvent(Id, oldCategoryId, newCategoryId));
        }

        public void Delete()
        {
            RaiseDomainEvent(new ProductDeletedEvent(Id, CategoryId));
        }
    }
}
