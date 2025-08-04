using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Domain.Products.ValueObjects
{
    public record CategoryId
    {
        public Guid Value { get; init; }

        public CategoryId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty", nameof(value));

            Value = value;
        }

        public static CategoryId Create(Guid value) => new(value);

        public static implicit operator Guid(CategoryId categoryId) => categoryId.Value;
        public static implicit operator CategoryId(Guid value) => new(value);
    }
}
