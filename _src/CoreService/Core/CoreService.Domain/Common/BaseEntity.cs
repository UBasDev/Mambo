using System.ComponentModel.DataAnnotations.Schema;

namespace CoreService.Domain.Common
{
    public abstract class BaseEntity<T> //Inherit all entities
    {
        protected BaseEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        protected T Id { get; set; }
        protected DateTimeOffset CreatedAt { get; set; }

        [NotMapped]
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

        protected void AddDomainEvents(IDomainEvent notification1) => domainEvents.Add(notification1);

        public void ClearDomainEvents1() => domainEvents.Clear();
    }
}