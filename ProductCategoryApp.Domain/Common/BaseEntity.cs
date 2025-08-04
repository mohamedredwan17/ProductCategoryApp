using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Domain.Common
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public Guid Id { get; protected set; } = Guid.NewGuid();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
