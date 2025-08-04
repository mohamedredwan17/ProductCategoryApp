using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Common;

namespace ProductCategoryApp.Domain.Categories.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public int ProductCount { get; private set; }

        private Category() { } 

        private Category(string name)
        {
            Name = name;
            ProductCount = 0;
        }

        public static Category Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            return new Category(name);
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            Name = name;
        }

        public void IncrementProductCount()
        {
            ProductCount++;
        }

        public void DecrementProductCount()
        {
            if (ProductCount > 0)
                ProductCount--;
        }
    }
}
