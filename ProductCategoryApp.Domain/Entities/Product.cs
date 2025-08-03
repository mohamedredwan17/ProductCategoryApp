using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Common;

namespace ProductCategoryApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product() { }

        public Product(string name, string description, decimal price, int categoryId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public void UpdateDetails(string name, string description, decimal price, int categoryId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
