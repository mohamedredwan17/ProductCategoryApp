using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCategoryApp.Domain.Common;
using ProductCategoryApp.Domain.Products.ValueObjects;

namespace ProductCategoryApp.Domain.Products.Events
{
    public record ProductCreatedEvent(Guid ProductId, CategoryId CategoryId) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
