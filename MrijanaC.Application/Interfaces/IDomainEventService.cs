using MirjanaC.Domain.Common;

namespace MirjanaC.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
