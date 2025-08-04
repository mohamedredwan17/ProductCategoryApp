using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Domain.Products.ValueObjects
{
    public record Price
    {
        public decimal Value { get; init; }
        public string Currency { get; init; }

        public Price(decimal value, string currency)
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative", nameof(value));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty", nameof(currency));

            Value = value;
            Currency = currency.ToUpperInvariant();
        }

        public static Price Create(decimal value, string currency) => new(value, currency);
    }
}