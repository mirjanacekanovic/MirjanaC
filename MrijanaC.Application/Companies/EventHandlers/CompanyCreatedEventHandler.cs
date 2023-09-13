using MediatR;
using Microsoft.Extensions.Logging;
using MirjanaC.Application.Common.Models;
using MirjanaC.Domain.Events;

namespace MirjanaC.Application.Companies.EventHandlers
{
    public class CompanyCreatedEventHandler : INotificationHandler<DomainEventNotification<CompanyCreatedEvent>>
    {
        private readonly ILogger<CompanyCreatedEventHandler> _logger;

        public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CompanyCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
