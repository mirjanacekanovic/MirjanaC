using MirjanaC.Domain.Common.Interfaces;

namespace MirjanaC.Domain.Common
{
    public abstract class BaseEntity : IEntity, IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        public int Id { get; set; }
    }
}
