using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Abstractions
{

    [Index(nameof(KeyId), IsUnique = true)]
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected BaseEntity()
        {
            KeyId = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            KeyId = id;
        }

        [Key]
        public int Id { get; set; }

        public Guid KeyId { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
